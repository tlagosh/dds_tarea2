namespace EscobaServidor;

public class Jugadores
{
    private List<Jugador> _jugadores;

    public Jugadores(int numJugadores, List<string> nombresJugadores)
    {
        _jugadores = new List<Jugador>();
        for (int i = 0; i < numJugadores; i++)
        {
            _jugadores.Add(new Jugador(nombresJugadores[i]));
        }
    }

    public Jugador ObtenerJugador(int idJugador) => _jugadores[idJugador];

    public int CantidadJugadores() => _jugadores.Count();
    
    public void RepartirCartas(int cantidadInicialCartas, PilaCartas pilaCartas)
    {
        foreach (var jugador in _jugadores)
            pilaCartas.DarCartas(jugador, cantidadInicialCartas);
    }

    public int GetIdJugadorConMasPuntos()
    {
        int idJugadorConMasPuntos = 0;
        int puntosJugadorConMasPuntos = 0;
        for (int i = 0; i < _jugadores.Count(); i++)
        {
            if (_jugadores[i]._puntosJuego > puntosJugadorConMasPuntos)
            {
                puntosJugadorConMasPuntos = _jugadores[i]._puntosJuego;
                idJugadorConMasPuntos = i;
            }
        }
        return idJugadorConMasPuntos;
    }
    
    public bool AlguienTieneCartasEnMano() => _jugadores.Any(j => j.TieneCartasEnMano());

    public bool ExisteJugadorCon16Puntos() => _jugadores.Any(j => j._puntosJuego >= 16);

    public void ContarPuntos()
    {
        foreach (var jugador in _jugadores)
        {
            jugador._puntosJuego += jugador.PuntosTotalesMano();
            jugador.cartasGanadasMano.Clear();
        }
    }

}