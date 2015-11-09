using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace TableReservation.Common.DataServices
{
    public interface IDataService
    {
        XmlSerializer Serializer { get; set; }

        object GetObjectByID(Guid id);

        List<object> GetAllObjects();

        bool SaveObject(Guid id, object obj);

        bool SaveAllObjects(Dictionary<Guid, object> objects);

        bool DeleteObject(Guid id);

        bool DeleteAllObjects(Dictionary<Guid, object> objects);

        List<XElement> GetXElements(string fileName, string rootName, string elementName);
    }
}
