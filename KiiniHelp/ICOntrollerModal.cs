namespace KiiniHelp
{
    public delegate void DelegateAceptarModal();
    public delegate void DelegateLimpiarModal();
    public delegate void DelegateCancelarModal();
    public interface IControllerModal
    {
        event DelegateAceptarModal OnAceptarModal;
        event DelegateLimpiarModal OnLimpiarModal;
        event DelegateCancelarModal OnCancelarModal;
    }
}