 
import javax.xml.parsers.DocumentBuilderFactory;
import javax.xml.parsers.DocumentBuilder;
import org.w3c.dom.Document;
import org.w3c.dom.NodeList;
import org.w3c.dom.Node;
import org.w3c.dom.Element;
import java.io.File;
 
public class Test {
 
  public static void main(String argv[]) {
	  System.out.println("Coke: ");
	  	Coke();
	  System.out.println("Unilever: ");
	  	Unilever();
	  System.out.println("Nvidia: ");
	  	nvidia(); 
  }
  
  public static void Coke ()
  {

	    try {
	 
		File fXmlFile = new File("/Users/Vivek/Documents/Coke.xml");
		DocumentBuilderFactory dbFactory = DocumentBuilderFactory.newInstance();
		DocumentBuilder dBuilder = dbFactory.newDocumentBuilder();
		Document doc = dBuilder.parse(fXmlFile);
	 

		doc.getDocumentElement().normalize();
	 
		System.out.println("Root element :" + doc.getDocumentElement().getNodeName());
	 
		NodeList nList = doc.getElementsByTagName("rowset");
	 
		System.out.println("----------------------------");
	 
		for (int temp = 0; temp < nList.getLength(); temp++) {
	 
			Node nNode = nList.item(temp);
	 
			System.out.println("\nCurrent Element :" + nNode.getNodeName());
	 
			if (nNode.getNodeType() == Node.ELEMENT_NODE) {
	 
				Element eElement = (Element) nNode;
				System.out.println( eElement.getElementsByTagName("value").item(0).getTextContent());
				System.out.println( eElement.getElementsByTagName("value").item(22).getTextContent());
				System.out.println( eElement.getElementsByTagName("value").item(23).getTextContent());
				
	 
			}
		}
	    } catch (Exception e) {
		e.printStackTrace();
	    }
  }
  
  public static void Unilever()
  {

	    try {
	 
		File fXmlFile = new File("/Users/Vivek/Documents/unilever.xml");
		DocumentBuilderFactory dbFactory = DocumentBuilderFactory.newInstance();
		DocumentBuilder dBuilder = dbFactory.newDocumentBuilder();
		Document doc = dBuilder.parse(fXmlFile);
	 

		doc.getDocumentElement().normalize();
	 
		System.out.println("Root element :" + doc.getDocumentElement().getNodeName());
	 
		NodeList nList = doc.getElementsByTagName("rowset");
	 
		System.out.println("----------------------------");
	 
		for (int temp = 0; temp < nList.getLength(); temp++) {
	 
			Node nNode = nList.item(temp);
	 
			System.out.println("\nCurrent Element :" + nNode.getNodeName());
	 
			if (nNode.getNodeType() == Node.ELEMENT_NODE) {
	 
				Element eElement = (Element) nNode;
				System.out.println( eElement.getElementsByTagName("value").item(0).getTextContent());
				System.out.println( eElement.getElementsByTagName("value").item(22).getTextContent());
				System.out.println( eElement.getElementsByTagName("value").item(23).getTextContent());
				
	 
			}
		}
	    } catch (Exception e) {
		e.printStackTrace();
	    }
  }
  
  public static void nvidia()
  {

	    try {
	 
		File fXmlFile = new File("/Users/Vivek/Documents/nvidia.xml");
		DocumentBuilderFactory dbFactory = DocumentBuilderFactory.newInstance();
		DocumentBuilder dBuilder = dbFactory.newDocumentBuilder();
		Document doc = dBuilder.parse(fXmlFile);
	 

		doc.getDocumentElement().normalize();
	 
		System.out.println("Root element :" + doc.getDocumentElement().getNodeName());
	 
		NodeList nList = doc.getElementsByTagName("rowset");
	 
		System.out.println("----------------------------");
	 
		for (int temp = 0; temp < nList.getLength(); temp++) {
	 
			Node nNode = nList.item(temp);
	 
			System.out.println("\nCurrent Element :" + nNode.getNodeName());
	 
			if (nNode.getNodeType() == Node.ELEMENT_NODE) {
	 
				Element eElement = (Element) nNode;
				System.out.println( eElement.getElementsByTagName("value").item(0).getTextContent());
				System.out.println( eElement.getElementsByTagName("value").item(22).getTextContent());
				System.out.println( eElement.getElementsByTagName("value").item(23).getTextContent());
				
	 
			}
		}
	    } catch (Exception e) {
		e.printStackTrace();
	    }
  }
}