# The DICOM Display System Management Service allows an Application Entity to retrieve Display Subsystem paramters from a peer by using the N-GET service.
# -*- coding:utf-8 -*-
from pynetdicom import AE, evt, debug_logger
from pynetdicom.sop_class import DisplaySystemSOPClass

from my_code import create_attribute_list
import pydicom

debug_logger()

# Implement the handler for evt.EVT_N_GET
def handle_get(event):
    """Hanlde a N-GET request event"""
    attr = event.request.AttributeIdentifierList
    dataset = create_attribute_list(attr)   # User defined function to generate the required attribute list dataset
    # dataset = pydicom.Dataset()
    # dataset.attr = "UIH"
    # Return success status and dataset
    return 0x0000, dataset


handlers = [(evt.EVT_N_GET, handle_get)]

# Initialise the Application Entity and specify the listen port
ae = AE()

# Add the supported presentation context
ae.add_supported_context(DisplaySystemSOPClass)

# Start listening for incoming association requests
ae.start_server(('', 11112), evt_handlers=handlers)
