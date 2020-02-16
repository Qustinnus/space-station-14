using Robust.Shared.GameObjects;
using Content.Server.GameObjects.EntitySystems;
using Content.Shared.GameObjects;
using Robust.Shared.IoC;

namespace Content.Server.GameObjects.Components
{
    /// <summary>
    ///     Allows an object with a InventoryComponent to be stripped from its items.
    /// </summary>
    [RegisterComponent]
    [ComponentReference(typeof(IActivate))]
    public class StrippingComponent : SharedStrippingComponent, IActivate
    {
        /// <inheritdoc />

        [Dependency] private readonly IGameHud _gameHud;

        void IActivate.Activate(ActivateEventArgs args)
        {
            Owner.TryGetComponent(out ClientStrippingComponent clientInventory);

            var menu = clientInventory.InterfaceController.Window;

            if (menu.IsOpen)
            {
                if (menu.IsAtFront())
                {
                    _setOpenValue(menu, false);
                }
                else
                {
                    menu.MoveToFront();
                }
            }
            else
            {
                _setOpenValue(menu, true);
            }
        }
        private void _setOpenValue(SS14Window menu, bool value)
        {
            if (value)
            {
                _gameHud.InventoryButtonDown = true;
                menu.OpenCentered();
            }
            else
            {
                _gameHud.InventoryButtonDown = false;
                menu.Close();
            }
        }


    }

}
