using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace TreeViewMVVM.Commands
{
    public class Manager
    {
        BinaryFormatter formatter = new BinaryFormatter();

        public void Serialize(ObservableCollection<TDL> tdls, string fileName)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream stream = new FileStream(fileName, FileMode.Create))
            {
                formatter.Serialize(stream, tdls);
            }
        }

        public ObservableCollection<TDL> Deserialize(string fileName)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream stream = new FileStream(fileName, FileMode.Open))
            {
                return (ObservableCollection<TDL>)formatter.Deserialize(stream);
            }
        }
    }
}
