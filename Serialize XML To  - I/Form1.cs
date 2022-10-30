/**
 * lufer
 * Serializar Documentos XML em:
 *  XML
 *      System.Xml; System.Xml.Serialization;
 *  JSON
 *  Objetos
 *
 *  XML to Objects (xml2C#)
 *  http://xmltocsharp.azurewebsites.net/
 *  Ver
 *  https://www.codeproject.com/Articles/1163664/Convert-XML-to-Csharp-Object
 * */
using System;
using System.IO;
using System.Windows.Forms;

using System.Xml;                       //Manipular XML
using System.Xml.Serialization;         //Serializar XML
using System.Xml.Linq;                  //Navegar em XML
using System.Web.Script.Serialization;  //Manipular JSON: Add Reference to System.Web.Extensions

//Newtonsoft
using Newtonsoft.Json;                  //Manipular JSON

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

        #region XML2JSON

        /// <summary>
        /// XML2Json
        /// Exemplos slides
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {

            Address obj = new Address();
            //h1
            //com System.Xml.Serialization; 
            var json = new JavaScriptSerializer().Serialize(obj);
            MessageBox.Show(json.ToString(), "JSON with System.Xml.JavaScriptSerializer" );

            //h2
            //ou com NewtonJson
            string aux = JsonConvert.SerializeObject(obj, Newtonsoft.Json.Formatting.None);
            MessageBox.Show(aux, "JSON with Newtonsoft.Json.JsonConvert");

            aux = JsonConvert.SerializeObject(obj, Newtonsoft.Json.Formatting.Indented);
            MessageBox.Show(aux, "JSON with Newtonsoft.Json.JsonConvert - Formated");

            //para ficheiro
            TextWriter writer = new StreamWriter(@"d:\temp\out.json");
            JsonSerializer serializer = new JsonSerializer();
            serializer.Serialize(writer, obj);
            writer.Close();
            MessageBox.Show("Send JSON to file" + @"d:\temp\out.json");
        }

        /// <summary>
        /// Class2JSON2XML2Class
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            Address obj = new Address();
            MessageBox.Show(obj.ToString(), "Class Address");

            //Serializing: Class2json
            var json = new JavaScriptSerializer().Serialize(obj);
            MessageBox.Show(json, "JSON with Newtonsoft.Json.JsonConvert");
            
            //Json2XML
            XNode node = JsonConvert.DeserializeXNode(json, "Root");
            MessageBox.Show(node.ToString(), "XML with Newtonsoft.Json.JsonConvert.");

            //Deserializing: json2class
            Address obj2 = JsonConvert.DeserializeObject<Address>(json);
            MessageBox.Show(obj2.ToString(), "Object with Newtonsoft.Json.DeserializeObject");
        }

        /// <summary>
        /// XML2JSON
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button5_Click(object sender, EventArgs e)
        {
            //File to XML DOM 
            XmlDocument doc = new XmlDocument();
            doc.Load(@"d:\temp\Data\out.xml");

            //XML DOM to JSON
            string json = JsonConvert.SerializeXmlNode(doc, Newtonsoft.Json.Formatting.Indented, true);
            //MessageBox.Show(json);
            textBox1.Text = json;
        }
        #endregion


        #region CLASS2XML
        /// <summary>
        /// Objetos2XML
        /// XML2File
        /// XML2String
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            //Class to XML
            Address aux = new Address();
            MessageBox.Show(aux.ToString(), "Classe Address");

            //para ficheiro
            XmlSerializer x = new XmlSerializer(aux.GetType());
            TextWriter writer = new StreamWriter(@"d:\temp\out.xml");
            x.Serialize(writer, aux);
            writer.Close();
            MessageBox.Show("File: " + @"d:\temp\out.xml");

            //Para string
            StringWriter stringWriter = new StringWriter();
            x.Serialize(stringWriter, aux);
            string serializedXML = stringWriter.ToString();
            //MessageBox.Show(serializedXML);
            textBox1.Text = serializedXML;
        }

        #endregion

        
        #region READ XML FILE

        /// <summary>
        /// Read File Button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button7_Click(object sender, EventArgs e)
        {
            var fileContent = string.Empty;
            var filePath = string.Empty;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "d:\\temp\\Data";
                openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    filePath = openFileDialog.FileName;

                    //Read the contents of the file into a stream
                    var fileStream = openFileDialog.OpenFile();

                    using (StreamReader reader = new StreamReader(fileStream))
                    {
                        fileContent = reader.ReadToEnd();
                    }
                }
            }
            textBox1.Text = fileContent;

            if (fileContent.Length > 0)
                MessageBox.Show(fileContent, "File Content at path: " + filePath, MessageBoxButtons.OK);
        }
        #endregion

        #region FILE2XML
        /// <summary>
        /// File to XML DOM
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button8_Click(object sender, EventArgs e)
        {
            //XML to XML DOM
            XmlDocument doc = new XmlDocument();
            doc.Load(@"d:\temp\Data\out.xml");
            //MessageBox.Show("Root Element:" + doc.DocumentElement.OuterXml);
            textBox1.Text = doc.DocumentElement.OuterXml;
        }
        #endregion

        #region XML Navigation
        /// <summary>
        /// XML Navigation
        /// Adapted from https://csharp.net-tutorials.com/xml/reading-xml-with-the-xmldocument-class/
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button6_Click(object sender, EventArgs e)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("http://www.ecb.int/stats/eurofxref/eurofxref-daily.xml");
            string aux = "";
            foreach (XmlNode xmlNode in xmlDoc.DocumentElement.ChildNodes[2].ChildNodes[0].ChildNodes)
                aux += xmlNode.Attributes["currency"].Value + ": " + xmlNode.Attributes["rate"].Value + "\n\r";
            textBox1.Text = aux;
        }
        #endregion

        /// <summary>
        /// Close the Form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Serializar/Desserializar com Newtonsoft.Json
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button9_Click(object sender, EventArgs e)
        {
            Book[] books = new Book[3];
            Author author1 = new Author(27, "XXXX yyyyy", "Author", books);
            //Serializar
            string objectDeserialized = JsonConvert.SerializeObject(author1);
            textBox1.Text = objectDeserialized;

            //Desserializar
            Author author2 = JsonConvert.DeserializeObject<Author>(objectDeserialized);

        }
    }
}
