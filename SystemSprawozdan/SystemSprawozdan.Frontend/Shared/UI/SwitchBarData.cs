namespace SystemSprawozdan.Frontend.Shared.UI
{
	public class SwitchBarData<T>
	{
		public string Text { get; set; }
		public T Value { get; set; }
		public bool IsActive { get; set; } = false;
	}
}
