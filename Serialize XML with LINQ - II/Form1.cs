/**
 * lufer & Oscar Ribeiro
 * Serializar Documentos XML com  LINQ
 * 
 * */
using System;
using System.IO;
using System.Windows.Forms;

using System.Xml.Linq;                  //Navegar em XML
using System.Linq;
using System.Xml;

using System.Collections.Generic;

using System.Text;
using System.Xml.Serialization;
using System.Xml.XPath;

namespace SerializeTo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #region Find
        /// <summary>
        /// Find...
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button5_Click(object sender, EventArgs e)
        {
            StringBuilder result = new StringBuilder();

            XElement root = XElement.Load(@"..\..\PurchaseOrder.xml");
            //LINQ2XML
            IEnumerable<XElement> address =
                from el in root.Elements("Address")
                where (string)el.Attribute("Type") == "Billing"
                select el;
            
            foreach (XElement el in address)
                foreach(XElement ch in el.Elements())
                    result.AppendLine(ch.ToString());

            textBox1.Text = result.ToString(); 
        }


        #endregion

        #region Sort
        /// <summary>
        /// Sort...
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button6_Click(object sender, EventArgs e)
        {
            StringBuilder result = new StringBuilder();

            XElement co = XElement.Load("CustomersOrders.xml");
            var sortedElements =
                from c in co.Element("Orders").Elements("Order")
                orderby (string)c.Element("ShipInfo").Element("ShipPostalCode"),
                        (DateTime)c.Element("OrderDate")
                select new
                {
                    CustomerID = (string)c.Element("CustomerID"),
                    EmployeeID = (string)c.Element("EmployeeID"),
                    ShipPostalCode = (string)c.Element("ShipInfo").Element("ShipPostalCode"),
                    OrderDate = (DateTime)c.Element("OrderDate")
                };

            //Show results
            foreach (var r in sortedElements)
                result.AppendLine(String.Format("CustomerID:{0} EmployeeID:{1} ShipPostalCode:{2} OrderDate:{3:d}",r.CustomerID, r.EmployeeID, r.ShipPostalCode, r.OrderDate));

            textBox1.Text = result.ToString();
        }
        #endregion

        #region LINQ2XML
        /// <summary>
        /// LINQ to read XML to XML DOM
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            StringBuilder result = new StringBuilder();

            //Load XML File
            //XDocument xdoc = XDocument.Load("PurchaseOrder.xml");
            //ou
            XElement rootElement = XElement.Load(@"..\..\PurchaseOrder.xml");
            //Run query
            foreach (XElement level1Element in rootElement.Elements("Items"))
            {             
                foreach (XElement level2Element in level1Element.Elements("Item"))
                {
                    result.Append("Attr: " + level2Element.Attribute("PartNumber").Value);
                    result.AppendLine(" - " + level2Element.Element("ProductName").Value);
                }
            }
            textBox1.Text = result.ToString();
        }

        #endregion
   
        #region READFILE

        /// <summary>
        /// LINQ to read File to XML
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button7_Click(object sender, EventArgs e)
        {
            var fileContent = string.Empty;
            var filePath = string.Empty;

            // Load the XML file from our project directory containing the purchase orders
            var filename = "PurchaseOrder.xml";
            var currentDirectory = @"..\..\";//Directory.GetCurrentDirectory();
            var purchaseOrderFilepath = Path.Combine(currentDirectory, filename);

            XElement purchaseOrder = XElement.Load(purchaseOrderFilepath);

            IEnumerable<XElement> pricesByPartNos = from item in purchaseOrder.Descendants("Item")
                                                    where (int)item.Element("Quantity") * (decimal)item.Element("USPrice") > 0
                                                    orderby (string)item.Element("PartNumber")
                                                    select item;

            //h2
            //IEnumerable<string> partNos = purchaseOrder.Descendants("Item").Select(x => (string)x.Attribute("PartNumber"));

            string aux = "";
            foreach (XElement result in pricesByPartNos)
            {
                aux += (result.Value + "\r\n");
            }
            textBox1.Text = aux;


        }
        #endregion

        #region CreateXML

        /// <summary>
        /// Create XML
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button8_Click(object sender, EventArgs e)
        {
            XElement contacts =
            new XElement("Contacts",
                new XElement("Contact",
                    new XElement("Name", "Patrick Hines"),
                    new XElement("Phone", "206-555-0144",
                        new XAttribute("Type", "Home")),
                    new XElement("phone", "425-555-0145",
                        new XAttribute("Type", "Work")),
                    new XElement("Address",
                        new XElement("Street1", "123 Main St"),
                        new XElement("City", "Mercer Island"),
                        new XElement("State", "WA"),
                        new XElement("Postal", "68042")
                    )
                )
            );
            
            textBox1.Text = contacts.ToString();
        }
        #endregion

        #region Navigate
        /// <summary>
        /// Navigate in XML with LINQ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button9_Click(object sender, EventArgs e)
        {
            XDocument doc = new XDocument();
            doc = XDocument.Load("PurchaseOrder.xml");
            IEnumerable<XElement> childs = doc.Descendants().Where(x => x.Name ==
            "Item");
            foreach (XElement el in childs)
            {
                textBox1.AppendText(el.Value + Environment.NewLine);
            }
        }

        #endregion

        #region XML2CSV

        /// <summary>
        /// XML to CSV
        /// Adapted from
        /// https://docs.microsoft.com/en-us/dotnet/standard/linq/find-descendant-elements
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button10_Click(object sender, EventArgs e)
        {
            StringBuilder s = new StringBuilder();
            string delimeter = ",";

            //XDocument po = XDocument.Load(@"PurchaseOrder.xml");
            //IEnumerable<XElement> list1 = po.Root.Descendants("Address");
            //ou
            // XPath expression
            //IEnumerable<XElement> list2 = po.XPathSelectElements("//Address");
            //ou
            List<XElement> list = XDocument.Load(@"PurchaseOrder.xml").Descendants("Address").ToList();
            foreach(XElement x in list)
            {
                s.Append(x.Element("Name").Value + delimeter +
                         x.Element("Country").Value + "\r\n");
            }
            //grava em ficheiro csv
            StreamWriter sw = new StreamWriter(@"d:\temp\data.csv");
            sw.WriteLine(s.ToString());
            sw.Close();
            
        }

        #endregion


        #region Object2XML
        /// <summary>
        /// Customizing Object to XML
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button11_Click(object sender, EventArgs e)
        {
            MyCar jbc = new MyCar();
            jbc.canFly = true;
            jbc.canSubmerge = false;
            jbc.theRadio.stationPresets = new double[] { 89.3, 105.1, 97.1 };
            jbc.theRadio.hasTweeters = true;

            XmlSerializer xs = new XmlSerializer(typeof(MyCar));
            Stream fStream = new FileStream(@"d:\temp\CarData.xml", FileMode.Create, FileAccess.Write, FileShare.None);
            xs.Serialize(fStream, jbc);
            fStream.Close();
        }

        #endregion

        #region List2XML
        /// <summary>
        /// Object List 2 XML
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button12_Click(object sender, EventArgs e)
        {
            Department dept = new Department();
            dept.Name = "IT";
            dept.Employees.Add(new Employee("Pedro"));
            dept.Employees.Add(new Employee("Joana"));
            dept.Employees.Add(new Employee("Joaquim"));
            XmlSerializer serializer = new XmlSerializer(dept.GetType());
            using (StreamWriter writer = new StreamWriter(@"d:\temp\Department.xml"))
            {
                serializer.Serialize(writer, dept);
            }
        }
        #endregion
    }

    #region XML_TO_C#
    //https://json2csharp.com/xml-to-csharp
    // using System.Xml.Serialization;
    // XmlSerializer serializer = new XmlSerializer(typeof(PurchaseOrder));
    // using (StringReader reader = new StringReader(xml))
    // {
    //    var test = (PurchaseOrder)serializer.Deserialize(reader);
    // }

    [XmlRoot(ElementName = "Address")]
    public class Address
    {

        [XmlElement(ElementName = "Name")]
        public string Name { get; set; }

        [XmlElement(ElementName = "Street")]
        public string Street { get; set; }

        [XmlElement(ElementName = "City")]
        public string City { get; set; }

        [XmlElement(ElementName = "State")]
        public string State { get; set; }

        [XmlElement(ElementName = "Zip")]
        public int Zip { get; set; }

        [XmlElement(ElementName = "Country")]
        public string Country { get; set; }

        [XmlAttribute(AttributeName = "Type")]
        public string Type { get; set; }

        [XmlText]
        public string Text { get; set; }

        public string PostalCode = "99999";
    }

    [XmlRoot(ElementName = "Item")]
    public class Item
    {

        [XmlElement(ElementName = "ProductName")]
        public string ProductName { get; set; }

        [XmlElement(ElementName = "Quantity")]
        public int Quantity { get; set; }

        [XmlElement(ElementName = "USPrice")]
        public double USPrice { get; set; }

        [XmlElement(ElementName = "Comment")]
        public string Comment { get; set; }

        [XmlAttribute(AttributeName = "PartNumber")]
        public string PartNumber { get; set; }

        [XmlText]
        public string Text { get; set; }

        [XmlElement(ElementName = "ShipDate")]
        public DateTime ShipDate { get; set; }
    }

    [XmlRoot(ElementName = "Items")]
    public class Items
    {

        [XmlElement(ElementName = "Item")]
        public List<Item> Item { get; set; }
    }

    [Serializable]
    [XmlRoot(ElementName = "PurchaseOrder")]
    public class PurchaseOrder
    {

        [XmlElement(ElementName = "Address")]
        public List<Address> Address { get; set; }

        [XmlElement(ElementName = "DeliveryNotes")]
        public string DeliveryNotes { get; set; }

        [XmlElement(ElementName = "Items")]
        public Items Items { get; set; }

        [XmlAttribute(AttributeName = "PurchaseOrderNumber")]
        public int PurchaseOrderNumber { get; set; }

        [XmlAttribute(AttributeName = "OrderDate")]
        public DateTime OrderDate { get; set; }

        [XmlText]
        public string Text { get; set; }
    }
    #endregion
}
