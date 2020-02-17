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
        private ArrayList list = new ArrayList();

        /// <summary>
        /// Контруктор команды
        /// </summary>
        /// <param name="list">ArrayList из всех параметров[C,A,B,Aeq,Beq,lb,ub]</param>
        //public CommandsRunSimplexMethod()
        //{
        //    list = ViewModels.ViewModels.ViewModel.res;
        //}

        //TODO: Подумать про ограничение использования команды
        public bool CanExecute(object parameter)
        {
            //bool? res = parameter as bool?;
            //if (res != null)
            //    return (bool) res;
            //else
            //{
            //    return false;
            //}
            return true;
        }
        
        public void Execute(object parameter)
        {
            AsyncCommand();
        }

        private void AsyncCommand()
        {
            list.Clear();
            List<IElements> IElemen = new List<IElements>();

            IElemen = ViewModels.ViewModels.ViewModel.IElementses;

            if (list != null)
            {
                list = list.GetArrayListing(IElemen);
            }
            else
            {
                list = new ArrayList();
                list = list.GetArrayListing(IElemen);
            }

            double[] C = list[0] as double[];
            double[] B = list[2] as double[];
            double[] Beq = list[4] as double[];
            double[] lb = list[5] as double[];
            double[] ub = list[6] as double[];

            Array A = list[1] as Array;
            Array Aeq = list[3] as Array;

            ViewModels.ViewModels.ViewModel.Task = new Task(()=> 
            {
                new LogicalSimplexMethodRun().LogicalSimplexMethodRuns(C, A, B, Aeq, Beq, lb, ub);

            });
            ViewModels.ViewModels.ViewModel.Task.RunSynchronously();
            
#if DEBUG
            MessageBox.Show("Собрался классец с решением");
#endif


        }

        public event EventHandler CanExecuteChanged;
    }
}
