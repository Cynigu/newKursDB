using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kurs.models
{
    public class ForComboBox : ReactiveObject
    {
        private string _filter;
        public string Filter
        {
            get { return _filter; }
            set { this.RaiseAndSetIfChanged(ref _filter, value); }
        }
        public override string ToString()
        {
            return Filter;
        }
    }
}
