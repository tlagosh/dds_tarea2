namespace EscobaServidor;

public class Jugador
{
    private List<Carta> _cartasMano = new List<Carta>();

    public int _puntosJuego = 0;

    public string _nombre;

    public List<Carta> CartasMano
    {
        get { return _cartasMano; }
    }

    public Jugador(string nombre)
    {
        _nombre = nombre;
    }

    public void AgregarCartaAMano(Carta carta)
    {
        _cartasMano.Add(carta);
    }

    public void SacarCartaDeMano(Carta carta)
    {
        _cartasMano.Remove(carta);
    }
    
    public bool TieneCartasEnMano()
    {
        return _cartasMano.Any();
    }

    public int CartasEnMano()
    {
        return _cartasMano.Count;
    }

    public int Oros()
    {
        return _cartasMano.Count(carta => carta.EsOro());
    }

    public int Sietes()
    {
        return _cartasMano.Count(carta => carta.EsSiete());
    }

    public bool TieneSieteDeOro()
    {
        return _cartasMano.Any(carta => carta.EsSiete() && carta.EsOro());
    }

    public bool TieneCarta(Carta carta)
    {
        return _cartasMano.Any(cartaEnMano => cartaEnMano.Equals(carta));
    }

    public override string ToString()
    {
        string s = _nombre;
        return s;
    }
}