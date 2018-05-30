try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load("E:\\Books.xml");
                XmlNode root = xmlDoc.DocumentElement;
                foreach (XmlNode xmlNode in root.ChildNodes)
                {
                    foreach (XmlNode xmlElement in xmlNode.ChildNodes)
                    {
                        Console.WriteLine(xmlElement.InnerText);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        } 