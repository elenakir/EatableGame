using System.IO;
using System.Xml.Linq;
using UnityEngine;

public class XMLConverter : MonoBehaviour
{
    [SerializeField] private GameData _gameData;
    
    private string _path;

    private void Awake()
    {
        _path = Application.persistentDataPath + "/customconfig.xml";
        Load();
    }

    private void OnDestroy()
    {
        Save();
    }

    private void Save()
    {
        XElement root = new XElement("root");

        root.Add(new XElement("healthCount", _gameData.healthCount));
        root.Add(new XElement("secondsCount", _gameData.secondsCount));
        root.Add(new XElement("maxCombo", _gameData.maxCombo));

        XDocument document = new XDocument(root);

        File.WriteAllText(_path, document.ToString());
    }

    private void Load()
    {
        XElement root = null;

        if (!File.Exists(_path))
        {
            if (File.Exists(Application.persistentDataPath + "/customconfig.xml"))
            {
                root = XDocument.Parse(File.ReadAllText(Application.persistentDataPath + "/customconfig.xml")).Element("root");
            }
        }
        else
        {
            root = XDocument.Parse(File.ReadAllText(_path)).Element("root");

            _gameData.healthCount = int.Parse(root.Element("healthCount").Value);
            _gameData.secondsCount = int.Parse(root.Element("secondsCount").Value);
            _gameData.maxCombo = int.Parse(root.Element("maxCombo").Value);
        }
    }
}
