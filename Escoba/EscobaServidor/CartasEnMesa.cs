namespace EscobaServidor;

public class CartasEnMesa
{
    private List<Carta> _cartasEnMesa = new List<Carta>();

    public List<Carta> Cartas => _cartasEnMesa;
    
    
    public override string ToString()
    {
        string cartas = "";
        foreach (Carta carta in _cartasEnMesa)
            cartas += carta;
        return cartas;
    }
    
    public void Agregar(Carta carta)
    {
        _cartasEnMesa.Add(carta);
    }

    public void Sacar(Carta carta)
    {
        _cartasEnMesa.Remove(carta);
    }

    public int CantidadCartas()
    {
        return _cartasEnMesa.Count;
    }

    private bool HayCartasEnMesa() => _cartasEnMesa.Any();

    public List<List<Carta>> ObtenerTodasLasJugadas(Carta carta)
    {
        List<Carta> cartasEnJuego = new List<Carta>(_cartasEnMesa);
        cartasEnJuego.Add(carta);
        List<List<Carta>> jugadas = ObtenerJugadas(cartasEnJuego);

        return jugadas;
    }

    private List<List<Carta>> ObtenerJugadas(List<Carta> cartasEnJuego)
    {
        int n = cartasEnJuego.Count;
        int sum = 15;

        SubSetSum subSetSum = new SubSetSum();

        List<List<Carta>> cartasSumasDe15 = subSetSum.GetSubSetSums(cartasEnJuego, n, sum);

        return cartasSumasDe15;
    }
    
}