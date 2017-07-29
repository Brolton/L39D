using System;
using System.Xml;
using System.Collections;
using System.IO;
using System.Text;
//using Nekki.SF2.Core;
using System.Collections.Generic;
using UnityEngine;
//using Nekki.Utils;
//using Nekki.SF2.Core.DataValidation;

public static class XmlUtils {
    public const string Comment = "#comment";

    public static bool IsNodeComment(XmlNode p_node) {
        return p_node.NodeType == XmlNodeType.Comment;
    }

    static public int ParseInt(XmlAttribute p_attr, int p_defVal = 0) {
        if (p_attr == null) {
            return p_defVal;
        }

        int result;
        return int.TryParse(p_attr.Value, out result) ? result : p_defVal;
    }

    static public int AsInt(this XmlAttribute p_attr, int p_defVal = 0)
    {
        if (p_attr == null) {
            return p_defVal;
        }

        int result;
        return int.TryParse(p_attr.Value, out result) ? result : p_defVal;
    }

    static public long ParseLong(XmlAttribute p_attr, long p_defVal = 0) {
        if (p_attr == null) {
            return p_defVal;
        }

        long result;
        return long.TryParse(p_attr.Value, out result) ? result : p_defVal;
    }

    static public long AsLong(this XmlAttribute p_attr, long p_defVal = 0) {
        if (p_attr == null) {
            return p_defVal;
        }

        long result;
        return long.TryParse(p_attr.Value, out result) ? result : p_defVal;
    }

    static public uint ParseUint(XmlAttribute p_attr, uint p_defVal = 0)
    {
        if (p_attr == null)
        {
            return p_defVal;
        }

        uint result;
        return uint.TryParse(p_attr.Value, out result) ? result : p_defVal;
    }

    static public uint AsUint(this XmlAttribute p_attr, uint p_defVal = 0)
    {
        if (p_attr == null)
        {
            return p_defVal;
        }

        uint result;
        return uint.TryParse(p_attr.Value, out result) ? result : p_defVal;
    }

    static public float ParseFloat(XmlAttribute p_attr, float p_defVal = 0f) {
        if (p_attr == null) {
            return p_defVal;
        }

        float result;
        return float.TryParse(p_attr.Value, out result) ? result : p_defVal;
    }

    static public float AsFloat(this XmlAttribute p_attr, float p_defVal = 0f) {
        if (p_attr == null) {
            return p_defVal;
        }

        float result;
        return float.TryParse(p_attr.Value, out result) ? result : p_defVal;
    }

    static public bool ParseBool(XmlAttribute p_attr, bool p_defVal = false) {
        if (p_attr == null) {
            return p_defVal;
        }

        int result;
        return int.TryParse(p_attr.Value, out result) ? result > 0 : p_defVal;
    }

    static public bool AsBool(this XmlAttribute p_attr, bool p_defVal = false) {
        if (p_attr == null) {
            return p_defVal;
        }

        int result;
        return int.TryParse(p_attr.Value, out result) ? result > 0 : p_defVal;
    }

    static public string ParseString(XmlAttribute p_attr, string p_defVal = null)
    {
        if (p_attr == null) {
            return p_defVal;
        }
        return p_attr.Value;
    }

    static public string AsString(this XmlAttribute p_attr, string p_defVal = null)
    {
        if (p_attr == null) {
            return p_defVal;
        }
        return p_attr.Value;
    }

    static public KeyValuePair<int, int> ParseMinMax(this XmlNode p_node, int p_defMin = 0, int p_defMax = 0)
    {
        if (p_node == null)
        {
            return new KeyValuePair<int, int>(p_defMin, p_defMax);
        }

        int min = p_node.Attributes["Min"].AsInt(p_defMin);
        int max = p_node.Attributes["Max"].AsInt(p_defMax);
        return new KeyValuePair<int, int>(min, max);
    }

    static public Vector2 ParseInOut(this XmlNode p_node, float p_defIn = 0, float p_defOut = 0)
    {
        var result = new Vector2(p_defIn, p_defOut);
        if (p_node != null)
        {
            result.x = p_node.Attributes["In"].AsFloat(p_defIn);
            result.y = p_node.Attributes["Out"].AsFloat(p_defOut);
        }
        return result;
    }

    static public XmlAttribute FirstAttribute(this XmlNode p_node)
    {
        if(p_node != null && p_node.Attributes.Count > 0)
        {
            return p_node.Attributes[0];
        }

        return null;
    }

    static public XmlAttribute LastAttribute(this XmlNode p_node)
    {
        if(p_node != null && p_node.Attributes.Count > 0)
        {
            return p_node.Attributes[p_node.Attributes.Count - 1]; 
        } 

        return null;
    }

    static public XmlAttribute Attribute(this XmlNode p_node, string p_attrName)
    {
        if(p_node != null)
        {
            return p_node.Attributes[p_attrName];
        }

        return null;
    }

    static public T ParseEnum<T>(XmlAttribute p_attr, T p_def)
	{
		if (p_attr == null) {
			return p_def;
		}
		try {
			T t = (T)Enum.Parse(typeof(T), p_attr.Value, true);
			return t;
		} catch {
			return p_def;
		}
	}

    static public bool Empty(this XmlAttribute p_attr)
    {
        if(p_attr == null)
        {
            return true;
        }

        return p_attr.Value.Equals(string.Empty);
    }

    static public XmlElement AppendChild(this XmlNode p_node, string p_name) {
        XmlDocument doc = p_node is XmlDocument ? (XmlDocument)p_node : p_node.OwnerDocument;
        XmlElement result = doc.CreateElement(p_name);
        p_node.AppendChild(result);
        return result;
    }

    static public XmlNode AppendCopy(this XmlNode p_node, XmlNode p_copy) {
        XmlDocument doc = p_node.OwnerDocument;
        if (doc == null) {
            doc = p_node as XmlDocument;
        }
        XmlNode result = doc.ImportNode(p_copy.Clone(), true);
        p_node.AppendChild(result);
        return result;
    }

    static public void AppendCopy(this XmlNode p_node, XmlAttribute p_copy) {
        ((XmlElement)p_node).SetAttribute(p_copy.Name, p_copy.Value);
    }

    static public XmlAttribute AppendAttribute(this XmlNode p_node, string attrName)
    {
        XmlAttribute attr = p_node.OwnerDocument.CreateAttribute(attrName);
        p_node.Attributes.Append(attr);
        return attr;
    }

    static public XmlAttribute PrependAttribute(this XmlNode p_node, string attrName)
    {
        XmlAttribute attr = p_node.OwnerDocument.CreateAttribute(attrName);
        p_node.Attributes.Prepend(attr);
        return attr;
    }

    static public XmlNode AppendNode(this XmlDocument p_doc, string nodeName)
    {
        XmlNode node = p_doc.CreateNode(XmlNodeType.Element, nodeName, null);
        p_doc.AppendChild(node);
        return node;
    }

    static public XmlNode AppendNode(this XmlNode p_node, string nodeName)
    {
        XmlNode node = p_node.OwnerDocument.CreateNode(XmlNodeType.Element, nodeName, null);
        p_node.AppendChild(node);
        return node;
    }

    static public XmlNode FindChildByAttribute(this XmlNode p_node, string name, string attrName)
    {
        if (p_node != null)
        {
            foreach (XmlNode node in p_node.ChildNodes)
            {
                if (node.Name.Equals(name))
                {
                    XmlAttribute attr = node.Attributes[attrName];
                    if (attr != null)
                    {
                        return node;
                    }
                }
            }
        }

        return null;
    }

    static public XmlNode FindChildByAttribute(this XmlNode p_node, string name, string attrName, string attrValue)
    {
        if (p_node != null)
        {
            foreach (XmlNode node in p_node.ChildNodes)
            {
                if(node.Name.Equals(name))
                {
                    XmlAttribute attr = node.FindAttributeWithValue(attrName, attrValue);
                    if(attr != null)
                    {
                        return node;
                    }
                }
            }
        }

        return null;
    }
    
    static public XmlAttribute FindAttributeWithValue(this XmlNode p_node, string attrName, string attrValue)
    {
        if (p_node != null)
        {
            foreach (XmlAttribute attr in p_node.Attributes)
            {
                if (attr.Name.Equals(attrName) && attr.Value.Equals(attrValue))
                {
                    return attr;
                }
            }
        }

        return null;
    }

    public enum OpenXmlType {
		Normal,
		ForcedResourced,
		ForcedExternal
	}

	public static XmlDocument OpenXMLDocumentFromBytes(byte[] p_bytes, bool p_ignoreComments = true)
	{
		XmlReaderSettings settings = new XmlReaderSettings();
		settings.IgnoreComments = p_ignoreComments;

		try {
			MemoryStream ms = new MemoryStream(p_bytes);
			using( XmlReader reader = XmlReader.Create(ms, settings))
			{
				XmlDocument result = OpenXMLDocument(reader);
				return result;
			}
		} catch  {
			return null;
		} 
	}

    public static XmlDocument OpenXMLDocumentFromString(string p_text, bool p_ignoreComments = true)
    {
        XmlReaderSettings settings = new XmlReaderSettings();
        settings.IgnoreComments = p_ignoreComments;

        try {
            XmlDocument result = OpenXMLDocFromString(p_text, settings);
            if (result == null) {
                Debug.LogError("Error open xml from string: " + p_text);
            }
            return result;
        } catch  {
            return null;
        } 
    }

    static public XmlDocument OpenXMLDocument(string p_path, string p_file = "", OpenXmlType p_openType = OpenXmlType.Normal, bool p_ignoreComments = true)
    {
        try {
            string filePath = p_path + (p_file != "" ? ("/" + p_file) : "");
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.IgnoreComments = p_ignoreComments;
            XmlDocument result = null;

//    		switch (p_openType) {
//    		case OpenXmlType.Normal:
//                if (p_path.StartsWith(SF2Paths.CurrentStorage)) {
//                    result = OpenXMLDocFromString(ResourceManager.GetTextFromExternal(filePath), settings);
//    			}
//                else {
//                    result = OpenXMLDocFromString(ResourceManager.GetText(filePath), settings);
//                }
//                break;
//    		case OpenXmlType.ForcedExternal:
//                result = OpenXMLDocFromString(ResourceManager.GetTextFromExternal(filePath), settings);
//                break;
//    		case OpenXmlType.ForcedResourced:
//                result = OpenXMLDocFromString(ResourceManager.GetTextFromResources(filePath), settings);
//                break;
//    		}
            if (result == null) {
                Debug.LogError("Error open xml " + filePath);
            }
            return result;

        }
        catch {
            return null;
        }

		return null;
    }

//	static public XmlDocument OpenXMLDocFromTextAsset(UnityEngine.TextAsset p_text, bool p_ignoreComments = true) {
//		XmlReaderSettings settings = new XmlReaderSettings();
//		settings.IgnoreComments = p_ignoreComments;
//
//		using( XmlReader reader = XmlReader.Create(new StringReader(p_text.text), settings))
//		{
//			XmlDocument result = OpenXMLDocument(reader);
//			if (result == null) {
//                DebugUtils.Dialog("Error open XML", true);
//			}
//			return result;
//		}
//
//	}

    static XmlDocument OpenXMLDocFromString(string p_data, XmlReaderSettings p_settings) {
        using( XmlReader reader = XmlReader.Create(new StringReader(p_data), p_settings))
        {
            return OpenXMLDocument(reader);
        }
    }

    static XmlDocument OpenXMLDocument(XmlReader p_reader) {
        try {
            XmlDocument document = new XmlDocument();
            document.Load(p_reader);
            return document;
        } catch (System.Exception ex) {
//            Log.Write(ex.ToString());
            return null;
        }
    }

//    static public void CopyXmlFromResources(string p_from, string p_to) {
//        XmlDocument doc = OpenXMLDocument(p_from);
//        doc.Save(p_to);
//    }

    public static void CopyXmlAndTrimSpaces(string p_from, string p_to)
    {
        XmlDocument doc = OpenXMLDocument(p_from, "", OpenXmlType.ForcedExternal, true);
        if (doc == null) {
            Debug.LogError("[XmlUtils]: try to trim spaces from incorrect xml - " + p_from);
            return;
        }

        try {
            XmlWriterSettings xwsSettings = new XmlWriterSettings();
            xwsSettings.Indent = false;
            xwsSettings.NewLineChars = String.Empty;
            using (XmlWriter xwWriter = XmlWriter.Create(p_to, xwsSettings)) {
                doc.Save(xwWriter);
            }
//            Debug.Log("[XmlUtils]: trim spaces xml - " + p_from);
        } catch (Exception exp) {
            Debug.LogException(exp);
        }
    }

    public static void TrimSpacesFromXML(string p_path)
    {
        CopyXmlAndTrimSpaces(p_path, p_path);
    }

//	public static bool IsFileValid(string p_path)
//	{
//		using (var xmlTextReader = new XmlTextReader(p_path)) {
//			try {
//				while (xmlTextReader.Read());
//            } catch (Exception exp) {
//				Log.Error("[Validation]: error to load file - {0}, exception - {1}", p_path, exp.Message);
//				return false;
//			}
//		}
//		return true;
//	}

    public static string GetPrettyOutXML(this XmlDocument p_document)
    {
        var sb = new System.Text.StringBuilder();

        var settings = new XmlWriterSettings();
        settings.OmitXmlDeclaration = true;
        settings.Indent = true;

        using (var xmlWriter = XmlWriter.Create(sb, settings)) {
            p_document.Save(xmlWriter);
        }

        return sb.ToString();
    }
}
