using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1_BatchRename
{
    class Data : INotifyPropertyChanged
    {
        private string _fileName;
        private string _newFilename;
        private string _path;
        private string _error;

        public string fileName
        {
            get { return _fileName; }
            set
            {
                _fileName = value;
                this.NotifyPropertyChanged("filename");
            }
        }
        public string newFilename
        {
            get { return _newFilename; }
            set
            {
                _newFilename = value;
                this.NotifyPropertyChanged("newFilename");
            }
        }
        public string path
        {
            get { return _path; }
            set
            {
                _path = value;
                this.NotifyPropertyChanged("path");
            }
        }

        public string error
        {
            get { return _error; }
            set
            {
                _error = value;
                this.NotifyPropertyChanged("error");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }
}
