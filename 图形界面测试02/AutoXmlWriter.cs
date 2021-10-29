using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;

namespace 图形界面测试02
{
    public class AutoXmlWriter
    {
        public AutoXmlWriter() { }
        public void CreateFileTest()
        {
            XmlDocument doc = new XmlDocument();
            XmlElement Root = doc.CreateElement("element");
            doc.AppendChild(Root);

            XmlElement child = doc.CreateElement("component");
            XmlAttribute attr1 = doc.CreateAttribute("Type");
            attr1.Value = "Switch";
            child.Attributes.Append(attr1);
            Root.AppendChild(child);

            doc.Save("test.xml");
        }

        public void CreateXmlFile(List<Element> list, string filePath)
        {
            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement("element");
            doc.AppendChild(root);

            foreach(Element e in list)
            {
                XmlElement child = doc.CreateElement("component");

                XmlAttribute Id = doc.CreateAttribute("ID");
                Id.Value = e.Id;
                child.Attributes.Append(Id);
                XmlAttribute Type = doc.CreateAttribute("Type");
                Type.Value = e.Type;
                child.Attributes.Append(Type);

                XmlElement name = doc.CreateElement("name");
                name.InnerText = e.Name;
                child.AppendChild(name);

                XmlElement location = doc.CreateElement("location");
                location.InnerText = e.Location;
                child.AppendChild(location);

                XmlElement angle = doc.CreateElement("angle");
                angle.InnerText = e.Angle.ToString();
                child.AppendChild(angle);

                XmlElement flip = doc.CreateElement("flip");
                flip.InnerText = e.Flip.ToString();
                child.AppendChild(flip);

                XmlElement condition = doc.CreateElement("condition");
                condition.InnerText = e.GetCondition.ToString();
                child.AppendChild(condition);

                XmlElement parent = doc.CreateElement("parent");
                parent.InnerText = e.ParentSwitch;
                child.AppendChild(parent);

                XmlElement child1 = doc.CreateElement("child");
                child1.InnerText = e.ChildSwitch;
                child.AppendChild(child1);

                root.AppendChild(child);
            }

            doc.Save(filePath);
        }
    }
}
