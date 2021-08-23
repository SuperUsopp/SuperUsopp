import sys

from pydicom import dcmread
from pydicom.dataset import Dataset
from pydicom.uid import generate_uid

from pynetdicom import AE, evt, debug_logger
from pynetdicom.sop_class import (
    BasicGrayscalePrintManagementMetaSOPClass,
    BasicFilmSessionSOPClass,
    BasicFilmBoxSOPClass,
    BasicGrayscaleImageBoxSOPClass,
    PrinterSOPClass,
    PrinterSOPInstance,
    PresentationLUTSOPClass
)

debug_logger()

# The SOP Instance containing the grayscale image data to be printed
DATASET=dcmread("D:\\Local_Repo\\dicom\\dicom\\CodeExamples\\PrintManagementServiceExamples\\path\\to\\file.dcm")

def build_film_session():
    """
    Return an N_CREATE attribute list that can be used to create a Basic Film Session SOP Class instance
    """
    attr_list = Dataset()
    attr_list.NumberOfCopies = "1"          # IS
    attr_list.PrintPriority = "LOW"         # CS
    attr_list.MediumType = "PAPER"          # CS
    attr_list.FilmDestination = "SOMEWHERE" # CS
    attr_list.FilmSessionLabel = "TEST JOB" # LO
    attr_list.MemoryAllocation = ""         # IS
    attr_list.OwnerID = "PYNETDICOM"        # SH

    return attr_list

def build_film_box(session, filmsession_instanceuid):
    # The "film" consists of a single Image Box
    attr_list = Dataset()
    attr_list.ImageDisplayFormat = 'STANDARD\\1,1'
    attr_list.FilmOrientation = 'PORTRAIT'
    attr_list.FilmSizeID = 'A4'

    # Can only contain a single item, is a reference to the Film Session
    attr_list.ReferencedFilmSessionSequence = [Dataset()]
    item = attr_list.ReferencedFilmSessionSequence[0]
    item.ReferencedSOPClassUID = BasicFilmSessionSOPClass                 #session.SOPClassUID
    item.ReferencedSOPInstanceUID = filmsession_instanceuid               #session.SOPInstanceUID

    return attr_list

def build_image_box(im):
    attr_list = Dataset()
    attr_list.ImageBoxPosition = 1          # US

    # Zero or one item only
    attr_list.BasicGrayscaleImageSequence = [Dataset()]
    item = attr_list.BasicGrayscaleImageSequence[0]
    item.SamplesPerPixel = im.SamplesPerPixel
    item.PhotometricInterpretation = im.PhotometricInterpretation
    item.Rows = im.Rows
    item.Columns = im.Columns
    item.BitsAllocated = im.BitsAllocated
    item.BitsStored = im.BitsStored
    item.HighBit = im.HighBit
    item.PixelRepresentation = im.PixelRepresentation
    item.PixelData = im.PixelData

    return attr_list

def handle_n_er(event):
    """Ignore the N_EVENT_REPORT notification"""
    return 0x0000

handlers = [(evt.EVT_N_EVENT_REPORT, handle_n_er)]

ae = AE()
ae.add_requested_context(BasicGrayscalePrintManagementMetaSOPClass)
ae.add_requested_context(PresentationLUTSOPClass)
assoc = ae.associate('localhost', 105, evt_handlers=handlers,ae_title="Printer")


if assoc.is_established:
    # Step 1: Check the status of the printer
    # (2110, 0010) Printer Status
    # (2110, 0020) Printer Status Info
    # Because the association was negotiated using a presentation context with a Meta SOP Class
    # we need to use the 'meta_uid' keyword parameter to ensure we use the correct context
    status, attr_list = assoc.send_n_get(
        [0x21100010, 0x21100020],
        PrinterSOPClass,
        PrinterSOPInstance,
        meta_uid = BasicGrayscalePrintManagementMetaSOPClass)
    if status and status.Status == 0x0000:
        if getattr(attr_list, 'PrinterStatus', None) != "NORMAL":
            print("Printer status is not NORMAL")
            assoc.release()
            sys.exit()
        # else:
        #     print("Failed to get the printer status")
        #     assoc.release()
        #     sys.exit()
    else:
        print("Failed to get the printer status")
        assoc.release()
        sys.exit()
    print("Printer ready")



    # Step 2: Create 'Film Session' instance
    film_session_instance_uid = generate_uid()
    status, film_session = assoc.send_n_create(
        build_film_session(),
        BasicFilmSessionSOPClass,
        film_session_instance_uid,
        2,
        meta_uid=BasicGrayscalePrintManagementMetaSOPClass)
    if not status or status.Status != 0x0000:
        print("Createion of Film Session failed, releasing association")
        assoc.release()
        sys.exit()
    print("Film session created")



    # Step 3: Create "Film Box" and "Image Box(es)"
    film_box_instance_uid=generate_uid()
    status, film_box = assoc.send_n_create(
        build_film_box(film_session, film_session_instance_uid),
        BasicFilmBoxSOPClass,
        film_box_instance_uid,
        3,
        meta_uid=BasicGrayscalePrintManagementMetaSOPClass)
    if not status or status.Status != 0x0000:
        print("Creation of the Film Box failed, releasing assocaiton")
        assoc.release()
        sys.exit()
    print("Film Box created")
    


    # Step 4: Update the "Image Box" with the image data
    # Get the Image Box's SOP Class and SOP Instance UIDs
    item = film_box.ReferencedImageBoxSequence[0]
    tmp_image_box = build_image_box(DATASET)
    status, image_box = assoc.send_n_set(
        tmp_image_box,
        item.ReferencedSOPClassUID,
        item.ReferencedSOPInstanceUID,
        4,
        meta_uid=BasicGrayscalePrintManagementMetaSOPClass)
    if not status or status.Status!=0x0000:
        print("Updating the Image Box failed, releasing association")
        assoc.release()
        sys.exit()
    print("Updated the Image Box with the image data")



    # Step 5: Print the "Film Box"
    status, action_replay = assoc.send_n_action(
        None,
        1,
        BasicFilmBoxSOPClass,           #film_box.SOPClassUID,
        film_box_instance_uid,          #film_box.SOPInstanceUID,
        5,
        meta_uid=BasicGrayscalePrintManagementMetaSOPClass
    )
    if not status or status.Status != 0x0000:
        print("Printing the Film Box failed, releasing association")
        assoc.release()
        sys.exit()

    # The actual printing may occur after association release/abort
    print("Print command sent successfully")
    
    # Optional - Delete the Film Box
    status = assoc.send_n_delete(
        BasicFilmBoxSOPClass,           #film_box.SOPClassUID,
        film_box_instance_uid,           #film_box.SOPInstanceUID,
        msg_id=6,
        meta_uid=BasicGrayscalePrintManagementMetaSOPClass
    )

    # Optional - Delete the Film Session
    status = assoc.send_n_delete(
        BasicFilmSessionSOPClass,
        film_session_instance_uid,
        msg_id=7,
        meta_uid=BasicGrayscalePrintManagementMetaSOPClass
    )

    # Release the association
    assoc.release()
