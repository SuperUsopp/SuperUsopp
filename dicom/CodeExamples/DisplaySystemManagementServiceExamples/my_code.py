# -*- coding:utf-8 -*-
from pydicom import Dataset, DataElement
from pydicom.datadict import dictionary_VR

def create_attribute_list(attr):
    ds = Dataset()
    for item in attr:
        elem = DataElement(item, dictionary_VR(item), "UIH")
        ds.add(elem)
    return ds