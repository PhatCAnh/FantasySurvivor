using ArbanFramework.MVC;
namespace FantasySurvivor.Model
{
	public class UnitModel : Model<GameApp>
	{
		public static EventTypeBase dataChangedEvent = new EventTypeBase(nameof(UnitModel) + ".dataChanged");

		public UnitModel(EventTypeBase eventType) : base(dataChangedEvent)
		{
		}

		public UnitModel() : base(dataChangedEvent)
		{
		}

		public UnitModel(float moveSpeed) : base(dataChangedEvent)
		{
			this.moveSpeed = moveSpeed;
		}

		private float _moveSpeed;

		public float moveSpeed
		{
			get => _moveSpeed;
			set
			{
				if (moveSpeed != value)
				{
					_moveSpeed = value;
					RaiseDataChanged(nameof(moveSpeed));
				}
			}
		}
	}
}