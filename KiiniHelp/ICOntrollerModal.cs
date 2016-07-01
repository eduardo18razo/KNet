namespace KiiniHelp
{
    public delegate void DelegateAceptarModal();
    public delegate void DelegateCerrarModal();
    public interface IControllerModal
    {
        event DelegateAceptarModal OnAceptarModal;
        event DelegateCerrarModal OnCerraModal;
    }
}