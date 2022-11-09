namespace EscobaServidor;

public class Jugador
{
    private List<Carta> _cartasMano = new List<Carta>();

    public List<Carta> cartasGanadasMano = new List<Carta>();

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

    public void AgregarCartaAGanadas(Carta carta)
    {
        cartasGanadasMano.Add(carta);
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
        return cartasGanadasMano.Count();
    }
    public int Oros()
    {
        return cartasGanadasMano.Count(carta => carta.EsOro());
    }

    public int Sietes()
    {
        return cartasGanadasMano.Count(carta => carta.EsSiete());
    }

    public bool TieneSieteDeOro()
    {
        return cartasGanadasMano.Any(carta => carta.EsSiete() && carta.EsOro());
    }

    public override string ToString()
    {
        string s = _nombre;
        return s;
    }
}