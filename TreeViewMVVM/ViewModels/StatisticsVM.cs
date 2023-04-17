using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeViewMVVM.ViewModels
{
    public class StatisticsVM
    {
        public int DueToday { get; set; }
        public int DueTomorrow { get; set; }
        public int Overdue { get; set; }
        public int Done { get; set; }
        public int ToBeDone { get; set; }
        public StatisticsVM(ObservableCollection<TDL> ItemsCollection)
        {
            DueToday = 0;
            DueTomorrow = 0;
            Overdue = 0;
            Done = 0;
            ToBeDone = 0;
            DateTime dateTime = DateTime.Now;
            foreach (var tdl in ItemsCollection)
            {
                if (tdl.SubTasks.Count != 0)
                    foreach (var tdlTask in tdl.SubTasks)
                    {
                        if (tdlTask.TaskStatus)
                        {
                            Done++;
                        }
                        else
                        {
                            ToBeDone++;

                            var dl = tdlTask.TaskDeadline.Date.CompareTo(dateTime.Date);
                            if (dl == 1 && dateTime.Date.AddDays(1) == tdlTask.TaskDeadline.Date)
                                DueTomorrow++;

                            switch (dl)
                            {
                                case 0: DueToday++; break;
                                case -1: Overdue++; break;
                            }
                        }
                    }
                if (tdl.SubTDLs.Count != 0)
                    foreach (var subtdl in tdl.SubTDLs)
                    {
                        if (subtdl.SubTasks.Count != 0)
                            foreach (var subTdlTask in subtdl.SubTasks)
                            {
                                if (subTdlTask.TaskStatus)
                                    Done++;
                                else
                                {
                                    ToBeDone++;

                                    var dl = subTdlTask.TaskDeadline.Date.CompareTo(dateTime.Date);
                                    if (dl == 1 && dateTime.Date.AddDays(1) == subTdlTask.TaskDeadline.Date)
                                        DueTomorrow++;

                                    switch (dl)
                                    {
                                        case 0: DueToday++; break;
                                        case -1: Overdue++; break;
                                    }
                                }
                            }
                    }
            }
        }
    }
}
