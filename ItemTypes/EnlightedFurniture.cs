using Items;
using Saver;
using Worlding;

namespace ItemTypes
{
    public class EnlightedFurniture : WorldItem, IFurniture, IEnlighted
    {
        public Switch Switch { get; private set; }
        public bool IsExternal { get; private set; }

        public EnlightedFurniture(string id, uint space, uint weight)
            : base(id, space, weight)
        {
            IsExternal = false;
            Switch = new Switch(false);
        }

        protected override object clone()
        {
            var clone = (EnlightedFurniture)instanciateClone();

            if (Switch.IsOn) clone.Switch.TurnOn();

            return clone;
        }

        protected virtual object instanciateClone() =>
            new EnlightedFurniture(Id, Space, Weight);

        protected override void load(Save save)
        {
            if (save.GetBool(nameof(Switch))) Switch.TurnOn();
            loadParticular(save);
        }

        protected virtual void loadParticular(Save save) { }

        protected override Save save()
        {
            var save = new Save(Id)
                .With(nameof(Switch), Switch.IsOn);
            saveParticular(save);

            return save;
        }

        protected virtual void saveParticular(Save save) { }
    }
}
