using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Ribbon;
using System.Windows.Input;
using ЧисленныМетоды.Models.SinplexMethod_GraphicInput;

namespace ЧисленныМетоды.Models.Commands
{
    public class CommandsRunSimplexMethod : ICommand
    {
        private ArrayList _list = new ArrayList();

        //TODO: Подумать про ограничение использования команды
        public bool CanExecute(object parameter)
        {
            return true;
        }
        
        public void Execute(object parameter)
        {
            AsyncCommand();
        }

        private void AsyncCommand()
        {
            _list.Clear();
            List<IElements> elemen = new List<IElements>();

            elemen = ViewModels.ViewModels.ViewModel.IElementses;

            if (_list != null)
            {
                _list = _list.GetArrayListing(elemen);
            }
            else
            {
                _list = new ArrayList();
                _list = _list.GetArrayListing(elemen);
            }

            double[] c = _list[0] as double[];
            double[] b = _list[2] as double[];
            double[] beq = _list[4] as double[];
            double[] lb = _list[5] as double[];
            double[] ub = _list[6] as double[];

            Array a = _list[1] as Array;
            Array aeq = _list[3] as Array;

            ViewModels.ViewModels.ViewModel.Task = new Task(()=> 
            {
                new LogicalSimplexMethodRun().LogicalSimplexMethodRuns(c, a, b, aeq, beq, lb, ub);

            });
            ViewModels.ViewModels.ViewModel.Task.RunSynchronously();
            
#if DEBUG
            MessageBox.Show("Собрался классец с решением");
#endif


        }

        public event EventHandler CanExecuteChanged;
    }
}
