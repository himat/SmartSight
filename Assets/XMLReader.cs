using UnityEngine;
using System.Collections;
using System.Xml;
using System.IO;

public class XMLReader : MonoBehaviour
{


		// Use this for initialization
		public static void Start ()
		{
		XmlDocument xmlDoc = new XmlDocument();
		xmlDoc.Load("StockInfo/Coke.xml");
		XmlNodeList nodeList = xmlDoc.DocumentElement.SelectNodes("/corefinancials/result/rowset/row/groups/group/rowset");
		string totalDebt = "", retainedEarnings = "", totalAssets="";
		foreach (XmlNode node in nodeList)
		{
			if(node.Attributes["field"].Value == "TotalDebt")
				totalDebt = node.InnerText;
//			MessageBox.Show("Total debt is " + totalDebt);

		};
			
		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}
}

