namespace EscobaServidor;

public class Jugador
{
    private List<Carta> _cartasMano = new List<Carta>();

    private List<Carta> _cartasGanadasMano = new List<Carta>();

    private int _puntosJuego = 0;

    private string _nombre;

    public List<Carta> CartasMano
    {
        get { return _cartasMano; }
    }

    public List<Carta> CartasGanadasMano
    {
        get { return _cartasGanadasMano; }
    }

    public int PuntosJuego
    {
        get { return _puntosJuego; }
        set { _puntosJuego = value; }
    }

    public string Nombre
    {
        get { return _nombre; }
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

    public void AgregarCartaAGanadas(Carta carta)
    {
        _cartasGanadasMano.Add(carta);
    }

    public int PuntosTotalesMano() 
    {
        int puntosMano = 0;
        if (TieneSieteDeOro())
            puntosMano += 1;
        if (Oros() >= 3)
            puntosMano += 1;
        if (Sietes() >= 3)
            puntosMano += 1;
        if (ContarCartasGanadasMano() > 20)
            puntosMano += 1;
        return puntosMano;
    }
    
    public bool TieneCartasEnMano()
    {
        return _cartasMano.Any();
    }

    public int CartasEnMano()
    {
        return _cartasMano.Count();
    }

    public int ContarCartasGanadasMano()
    {
        return _cartasGanadasMano.Count();
    }
    public int Oros()
    {
        return _cartasGanadasMano.Count(carta => carta.EsOro());
    }

    public int Sietes()
    {
        return _cartasGanadasMano.Count(carta => carta.EsSiete());
    }

    public bool TieneSieteDeOro()
    {
        return _cartasGanadasMano.Any(carta => carta.EsSiete() && carta.EsOro());
    }

    public override string ToString()
    {
        string s = _nombre;
        return s;
    }
}