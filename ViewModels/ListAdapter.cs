using Android.Views;
using Android.Widget;
using System.Collections.Generic;
using ToDoApp.Models.Entities.Interfaces;
using ToDoApp.Views;
using Context = Android.Content.Context;
using Task = ToDoApp.Models.Entities.Task;

namespace ToDoApp.ViewModels
{
    public class ListAdapter : BaseAdapter
    {

        private readonly List<Task> _taskList;
        private readonly ToDoMainView mainView;
        private readonly ITaskRepository _dataBaseService;

        public ListAdapter(ToDoMainView mainView, List<Task> taskList, ITaskRepository dataBaseService)
        {
            _taskList = taskList;
            this.mainView = mainView;
            _dataBaseService = dataBaseService;
        }

        public override int Count => _taskList.Count;

        public override Java.Lang.Object GetItem(int position)
        {
            return position;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            LayoutInflater inflater = (LayoutInflater)mainView.GetSystemService(Context.LayoutInflaterService);
            View view = inflater.Inflate(Resource.Layout.row, null);
            TextView txtTask = view.FindViewById<TextView>(Resource.Id.task_title);
            Button btnDelete = view.FindViewById<Button>(Resource.Id.btnDelete);
            txtTask.Text = _taskList[position].Title;
            btnDelete.Click += delegate
            {
                Task taskId = _taskList[position];
                _dataBaseService.DeleteTask(taskId);
                mainView.LoadTaskList(); // Reload Data  
            };
            return view;
        }
    }
}