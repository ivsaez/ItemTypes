using Items;
using Saver;
using Worlding;

namespace ItemTypes
{
    public class EnlightedItem : WorldItem, IEnlighted
    {
        public Switch Switch { get; private set; }

        public EnlightedItem(string id, uint space, uint weight)
            : base(id, space, weight)
        {
            Switch = new Switch(false);
        }

        protected override object clone()
        {
            var clone = (EnlightedItem)instanciateClone();

            if (Switch.IsOn) clone.Switch.TurnOn();

            return clone;
        }

        protected virtual object instanciateClone() =>
            new EnlightedItem(Id, Space, Weight);

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
