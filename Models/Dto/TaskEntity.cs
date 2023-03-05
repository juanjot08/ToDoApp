using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ToDoApp.Models.Dto
{
    public class TaskEntity
    {
        [PrimaryKey, AutoIncrement]
        public int Id { set; get; }

        public string Title { set; get; }

        public string TextDescription { set; get; }

        public int UserId { set; get; }

    }
}