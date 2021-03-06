# The DICOM Display System Management Service allows an Application Entity to retrieve Display Subsystem paramters from a peer by using the N-GET service.
# -*- coding:utf-8 -*-
from pynetdicom import AE, debug_logger
from pynetdicom.sop_class import DisplaySystemSOPClass, DisplaySystemSOPInstance
from pynetdicom.status import code_to_category


debug_logger()

# Initialse the Application Entity
ae = AE()

# Add a request presentation context
ae.add_requested_context(DisplaySystemSOPClass)

# Associate with the peer AE at IP 127.0.0.1 and port 11112
assoc = ae.associate('127.0.0.1', 11112)

if assoc.is_established:
    # Use the N-GET service to send the request, returns the response status a pydicom Dataset and the AttributeList dataset
    status, attr_list = assoc.send_n_get([(0x0008,0x0070)], 
                                         DisplaySystemSOPClass,
                                         DisplaySystemSOPInstance)

    # Check the status of the display system request
    if status:
        print("N-GET request status:0x{0:04x}".format(status.Status))

        # If the display system request succeeded the status category may be either success or warning
        category = code_to_category(status.Status)
        if category in ['Warning','Success']:
            # 'attr_list' is a pydicom Dataset containing attribute values
            print(category)
            print(attr_list)
    
    # Release the association
    assoc.release()
else:
    print('Association rejected, aborted or never connected')
