using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Chat.DomainModel.Domain
{
	public abstract class BaseModel : ViewModelBase
	{
		[Key]
		public Guid Id { get; set; }
	}

	public class ViewModelBase : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		public void RaisePropertyChanged(string propertyName = "") =>
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
}
