# -*- coding:utf-8 -*-

from pydicom.uid import ExplicitVRLittleEndian
from pynetdicom import AE, debug_logger
from pynetdicom.sop_class import CTImageStorage, VerificationSOPClass 

debug_logger()

ae = AE()
ae.add_supported_context(VerificationSOPClass, ExplicitVRLittleEndian)
ae.start_server(('', 11112), block=True)
print("The server has already been startup")
print("Add another comments to make conflict")
