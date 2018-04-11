using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqCore.DataModel
{
    public class TaskModel
    {
        public int Id;
        public string Author;
        public string Name;
        public string Content;
        public bool IsCanceled;
        public TaskModel CanceledBy;
        public IQueryable<LetterModel> Letters;
        public IQueryable<Device> Devices;
    }
    public class LetterModel
    {
        public int Id;
        public DateTime Date;
        public string Name;
        public TaskModel Task;
        public bool IsReport;
        public IQueryable<FileModel> Files;
    }
    public class FileModel
    {
        public int Id;
        public string Name;
        public string Extention;
        public LetterModel Letter;
    }
    public class Device
    {
        public int Id;
        public string Name;
        public IQueryable<TaskModel> Tasks;
    }
    public interface IDataModel
    {
        IQueryable<TaskModel> Tasks { get; }
        IQueryable<LetterModel> Letters { get; }
        IQueryable<FileModel> Files { get; }
        IQueryable<Device> Devices { get; }
    }
}
