using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1_BatchRename
{
    class StringOperationContraft
    {
    }

    public class Utils
    {
        public static string GetTailFile(string filename)
        {
            string result = "";

            int index = filename.LastIndexOf('.');
            if (index != -1)
            {
                result = filename.Substring(index + 1);
            }

            return result;
        }

        public static string UppperChar(string str, int index)
        {
            if (index < 0 || index >= str.Length)
            {
                return str;
            }

            string result = "";

            string first = str.Substring(0, index);
            string last = str.Substring(index + 1, str.Length - index - 1);
            string c = str.Substring(index, 1);
            c = c.ToUpper();
            result = first + c + last;

            return result;
        }

    }

    #region Common
    public class StringArgs { }

    public abstract class StringOperation : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public StringArgs Args { get; set; }

        public abstract string Operate(string origin);

        public abstract string Name { get; }
        public abstract string Description { get; }

        public abstract StringOperation Clone();

        public abstract void Config();
    }
    #endregion

    #region New Case
    public enum TypeCase
    {
        AllCharUpperCase,
        AllCharLowerCase,
        FirstCharUpperCase
    }

    public class NewCaseArgs : StringArgs, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;


        private TypeCase type;
        public TypeCase Type { get => type; set => type = value; }
    }

    public class NewCaseOperation : StringOperation, INotifyPropertyChanged
    {
        public override string Name => "New Case";

        public override string Description
        {
            get
            {
                var args = Args as NewCaseArgs;
                return $"Do {args.Type.ToString()}.";
            }
        }

        public override StringOperation Clone()
        {
            var origin_args = Args as NewCaseArgs;
            return new NewCaseOperation
            {
                Args = new NewCaseArgs
                {
                    Type = origin_args.Type
                }
            };
        }

        public override void Config()
        {
            var oldType = (this.Args as NewCaseArgs).Type;
            var screen = new NewCaseDialog(oldType);

            screen.DimensionChanged += Screen_DimensionChanged;
            if (screen.ShowDialog() == true)
            {

            }
            else
            {
                (this.Args as NewCaseArgs).Type = oldType;
            }
        }

        private void Screen_DimensionChanged(TypeCase type)
        {
            (this.Args as NewCaseArgs).Type = type;
        }


        public override string Operate(string origin)
        {
            string result = "";
            var type = (Args as NewCaseArgs).Type;

            string tail = '.' + Utils.GetTailFile(origin);
            string origin_1 = origin.Replace(tail, ""); // remove tail

            switch (type)
            {
                case TypeCase.AllCharUpperCase:
                    {
                        result = (string)origin_1.ToUpper();
                        break;
                    }
                case TypeCase.AllCharLowerCase:
                    {
                        result = (string)origin_1.ToLower();
                        break;
                    }
                case TypeCase.FirstCharUpperCase:
                    {
                        result = (string)origin_1.ToLower();
                        string first = result.Substring(0, 1).ToUpper();
                        string last = result.Substring(1, result.Length - 1);
                        result = first + last;
                        break;
                    }
            }

            return result + tail;
        }
    }
    #endregion

    #region Replace
    public class ReplaceArgs : StringArgs, INotifyPropertyChanged
    {
        private string from;
        public string From { get => from; set => from = value; }

        private string to;
        public string To { get => to; set => to = value; }

        public event PropertyChangedEventHandler PropertyChanged;
    }

    public class ReplaceOperation : StringOperation, INotifyPropertyChanged
    {
        public override string Name => "Replace";

        public override string Description
        {
            get
            {
                var args = Args as ReplaceArgs;
                return $"Replace from '{args.From}' to '{args.To}'.";
            }
        }


        public override StringOperation Clone()
        {
            var origin_args = Args as ReplaceArgs;
            return new ReplaceOperation
            {
                Args = new ReplaceArgs
                {
                    From = origin_args.From,
                    To = origin_args.To
                }
            };
        }

        public override void Config()
        {

            var oldFrom = (this.Args as ReplaceArgs).From;
            var oldTo = (this.Args as ReplaceArgs).To;
            var screen = new ReplaceDialog(oldFrom, oldTo);

            screen.DimensionChanged += Screen_DimensionChanged;
            if (screen.ShowDialog() == true)
            {

            }
            else
            {
                (this.Args as ReplaceArgs).From = oldFrom;
                (this.Args as ReplaceArgs).To = oldTo;
            }
        }

        private void Screen_DimensionChanged(string from, string to)
        {
            (this.Args as ReplaceArgs).From = from;
            (this.Args as ReplaceArgs).To = to;
        }

        public override string Operate(string origin)
        {
            var args = Args as ReplaceArgs;
            string from = args.From;
            string to = args.To;

            return origin.Replace(from, to);
        }
    }
    #endregion
    #region Remove
    public class RemoveOperation : StringOperation, INotifyPropertyChanged
    {
        public override string Name => "Remove";

        public override string Description
        {
            get
            {
                var args = Args as ReplaceArgs;
                return $"Remove pattern '{args.From}'.";
            }
        }


        public override StringOperation Clone()
        {
            var origin_args = Args as ReplaceArgs;
            return new ReplaceOperation
            {
                Args = new ReplaceArgs
                {
                    From = origin_args.From,
                    To = origin_args.To
                }
            };
        }

        public override void Config()
        {

            var pattern = (this.Args as ReplaceArgs).From;
            var screen = new RemovePatternDialog(pattern);

            screen.DimensionChanged += Screen_DimensionChanged;
            if (screen.ShowDialog() == true)
            {

            }
            else
            {
                (this.Args as ReplaceArgs).From = pattern;

            }
        }

        private void Screen_DimensionChanged(string pattern)
        {
            (this.Args as ReplaceArgs).From = pattern;
        }

        public override string Operate(string origin)
        {
            var args = Args as ReplaceArgs;
            string from = args.From;
            string to = args.To;

            string tail = '.' + Utils.GetTailFile(origin);
            string origin_1 = origin.Replace(tail, ""); // remove tail

            return origin_1.Replace(from, to) + tail;
        }
    }

    #endregion

    #region Move
    public enum TypeMove
    {
        Start,
        End
    }

    public class MoveArgs : StringArgs, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private TypeMove type;
        public TypeMove Type { get => type; set => type = value; }
    }

    public class MoveOperation : StringOperation, INotifyPropertyChanged
    {
        public override string Name => "Move";

        public override string Description
        {
            get
            {
                var args = Args as MoveArgs;
                return $"Move {args.Type.ToString()}";
            }
        }


        public override StringOperation Clone()
        {
            var origin_args = Args as MoveArgs;
            return new MoveOperation
            {
                Args = new MoveArgs
                {
                    Type = origin_args.Type
                }
            };
        }

        public override void Config()
        {
            var oldType = (this.Args as MoveArgs).Type;
            var screen = new MoveDialog(oldType);

            screen.DimensionChanged += Screen_DimensionChanged;
            if (screen.ShowDialog() == true)
            {

            }
            else
            {
                (this.Args as MoveArgs).Type = oldType;
            }
        }

        private void Screen_DimensionChanged(TypeMove type)
        {
            (this.Args as MoveArgs).Type = type;
        }

        public override string Operate(string origin)
        {
            var type = (Args as MoveArgs).Type;
            string result = "";

            string tail = '.' + Utils.GetTailFile(origin);
            string origin_1 = origin.Replace(tail, ""); // remove tail

            switch (type)
            {
                case TypeMove.Start:
                    {
                        result = origin_1;
                        break;
                    }
                case TypeMove.End:
                    {
                        string isbn = origin_1.Substring(0, 13);
                        string name = origin_1.Substring(14);
                        result = name + " " + isbn;
                        break;
                    }
            }

            return result + tail;
        }
    }

    #endregion

    #region Unique Name
    public class UniqueNameArgs : StringArgs, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string tailFile;
        public string TailFile { get => tailFile; set => tailFile = value; }
    }

    public class UniqueNameOperation : StringOperation, INotifyPropertyChanged
    {
        public override string Name => "Unique Name";

        public override string Description
        {
            get
            {
                var args = Args as UniqueNameArgs;
                return $"Create unique file's name with tail is '{args.TailFile}'";
            }
        }

        public override StringOperation Clone()
        {
            var origin = Args as UniqueNameArgs;
            return new UniqueNameOperation
            {
                Args = new UniqueNameArgs
                {
                    TailFile = origin.TailFile
                }
            };
        }

        public override void Config()
        {

            var screen = new NewNameDialog();
            if (screen.ShowDialog() == true)
            {

            }
        }

        public override string Operate(string origin)
        {
            string result = "";
            (Args as UniqueNameArgs).TailFile = Utils.GetTailFile(origin);

            result = Guid.NewGuid().ToString() + "." + (Args as UniqueNameArgs).TailFile;

            return result;
        }
    }

    #endregion

    #region Fullname Normalize

    public class NormalizeArgs : StringArgs, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
    }

    public class NormalizeOperation : StringOperation, INotifyPropertyChanged
    {
        public override string Name => "Fullname Normalize";

        public override string Description
        {
            get
            {
                return "Do Fullname Normalize.";
            }
        }

        public override StringOperation Clone()
        {
            return new NormalizeOperation
            {
                Args = new NormalizeArgs()
            };
        }

        public override void Config()
        {

            var screen = new FullnameNormalizeDialog();
            if (screen.ShowDialog() == true)
            {

            }
        }

        public override string Operate(string origin)
        {
            string result = "";

            string tail = '.' + Utils.GetTailFile(origin);
            string origin_1 = origin.Replace(tail, ""); // remove tail

            // delete space from start & end
            string temp = origin_1.Trim();
            temp = temp.ToLower();


            // delete space between
            for (int i = 0; i < temp.Length - 1; i++)
            {
                if (temp[i] == ' ' && temp[i + 1] == ' ')
                {
                    temp = temp.Remove(i, 1);
                    i--;
                }
            }

            //
            temp = Utils.UppperChar(temp, 0);
            for (int i = 1; i < temp.Length; i++)
            {
                if (temp[i - 1] == ' ')
                {
                    temp = Utils.UppperChar(temp, i);
                }
            }

            result = temp;
            return result + tail;
        }
    }

    #endregion
}
