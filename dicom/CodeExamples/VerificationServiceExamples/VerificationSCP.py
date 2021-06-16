# -*- coding:utf-8 -*-
from pynetdicom import AE, debug_logger
from pynetdicom.sop_class import VerificationSOPClass


debug_logger()

# Initialise the Application Entity
ae = AE()

# Add the supported presentation context
ae.add_supported_context(VerificationSOPClass)

# Start listening for incoming association requests in blocking mode
ae.start_server(('127.0.0.1', 11112), block=True)
