# -*- coding:utf-8 -*-
import os

from pydicom import dcmread
from pydicom.dataset import Dataset

from pynetdicom import AE, evt, debug_logger
from pynetdicom.sop_class import PatientRootQueryRetrieveInformationModelFind


debug_logger()

# Implement the handler for evt.EVT_C_FIND
def handle_find(event):
    """Hanlde a C-FIND request event"""
    ds = event.identifier

    # Import stored SOP Instances
    instances = []
    fdir = r'D:\dicom\dicom\CodeExamples\BasicWorklistManagementServiceExamples\path\to\directory'
    for fpath in os.listdir(fdir):
        instances.append(dcmread(os.path.join(fdir, fpath)))

    if 'QueryRetrieveLevel' not in ds:
        # Failure
        yield 0XC000, None
        return

    if ds.QueryRetrieveLevel == 'PATIENT':
        if 'PatientName' in ds:
            if ds.PatientName not in ['*', '', '?']:
                matching = [
                    inst for inst in instances if inst.PatientName == ds.PatientName
                ]

    for instance in matching:
        # Check if C-CANCEL has been received
        if event.is_cancelled:
            yield(0XFE00, None)
            return
        
        identifier = Dataset()
        identifier.PatientName = instance.PatientName
        identifier.QueryRetrieveLevel = ds.QueryRetrieveLevel
        
        # Pending
        yield(0XFF00, identifier)

handlers = [(evt.EVT_C_FIND, handle_find)]

# Initialise the Application Entity and specify the listen port
ae = AE()

# Add the supported presentation context
ae.add_supported_context(PatientRootQueryRetrieveInformationModelFind)

# Start listening for incoming association requests
ae.start_server(('', 11112), evt_handlers=handlers)
