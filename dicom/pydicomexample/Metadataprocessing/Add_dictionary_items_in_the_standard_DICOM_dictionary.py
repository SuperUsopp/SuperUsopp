# -*- coding:utf-8 -*-
# This examples illustrates how to add dictionary items to the 'standard' DICOM dictionary dynamiclly.
from pydicom.datadict import DicomDictionary, keyword_dict
from pydicom.dataset import Dataset


print(__doc__)

# Define items as (VR, VM, description, is_retired flag, keyword), leave 'is_retired flag' blank
new_dict_items = {
    0x10011001: ('UL', '1', 'Test One', '', 'TestOne'),
    0x10011002: ('OB', '1', 'Test Two', '', 'TestTwo'),
    0x10011003: ('UI', '1', 'Test Three', '', 'TestThree'),
}

# Update the dictionary itself
DicomDictionary.update(new_dict_items)

# Update the reverse mapping from name to tag
new_names_dict = dict([(val[4], tag) for tag, val in new_dict_items.items()])
keyword_dict.update(new_names_dict)

# Test that it is working
ds = Dataset()
ds.TestOne = 42
ds.TestTwo = '12345'
ds.TestThree = '1.2.3.4.5'

# print(ds.top())
