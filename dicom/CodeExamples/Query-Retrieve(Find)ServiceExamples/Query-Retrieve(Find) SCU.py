# Associate with a peer DICOM Application Entity and request the SCP search for SOP Instance with a Patient Name matching CITIZEN^Jan using Patient Root Query/Retrieve Information Model-Find at the 'PATIENT' level
# -*- coding:utf-8 -*-
from pydicom import Dataset

from pynetdicom import AE, debug_logger
from pynetdicom.sop_class import PatientRootQueryRetrieveInformationModelFind

debug_logger()

ae = AE()
ae.add_requested_context(PatientRootQueryRetrieveInformationModelFind)

# Create our Identifier (query) dataset
ds = Dataset()
ds.PatientName = "CITIZEN^Jan"
ds.QueryRetrieveLevel = "PATIENT"

# Associate with the peer AE at IP 127.0.0.1 and port 11112
assoc = ae.associate('127.0.0.1', 11112)

if assoc.is_established:
    # Send the C-FIND request
    responses = assoc.send_c_find(ds, PatientRootQueryRetrieveInformationModelFind)
    for (status, identifier) in responses:
        if status:
            print("C-FIND query status: 0x{0:04X}".format(status.Status))
            print(identifier)
        else:
            print("Connection timed out, was aborted or received invalid response")
    
    # Release the association
    assoc.release()
else:
    print("Association rejected, aborted or never connected")
