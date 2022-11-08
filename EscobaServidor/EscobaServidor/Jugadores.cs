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

    public int CantidadJugadores() => _jugadores.Count;
    
    public void RepartirCartas(int cantidadInicialCartas, PilaCartas pilaCartas)
    {
        foreach (var jugador in _jugadores)
            pilaCartas.DarCartas(jugador, cantidadInicialCartas);
    }
    
    public bool AlguienTieneCartasEnMano() => _jugadores.Any(j => j.TieneCartasEnMano());

    public bool ExisteJugadorCon16Puntos() => _jugadores.Any(j => j._puntosJuego >= 16);

}