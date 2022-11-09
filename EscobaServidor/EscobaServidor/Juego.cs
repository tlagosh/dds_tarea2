namespace EscobaServidor;

public class Juego
{
    private const int NumJugadores = 2;
    private const int CantidadInicialCartas = 3;

    private Jugadores _jugadores;
    private int _idJugadorTurno = 0;

    private int _idGanador = 0;
    private CartasEnMesa _cartasEnMesa;
    private Vista _vistaJugador1 = new VistaSocket(8001);
    private Vista _vistaJugador2;
    //private Vista _vistaConsola = new VistaConsola();
    private bool _modoDeJuegoServidor = false;
    public Vista GetVistaActual() {
        if (!_modoDeJuegoServidor) {
            return _vistaJugador1;
        } else {
            if (_idJugadorTurno == 0) {
                return _vistaJugador1;
            } else {
                return _vistaJugador2;
            }
        }
    }

    public Juego()
    {
        CrearJugadores();
    }
    private void CrearJugadores() => _jugadores = new Jugadores(NumJugadores, new List<string> { "Jugador 1", "Jugador 2" });

    private void Inicio() 
    {
        GetVistaActual().MensajeBienvenida();
        _modoDeJuegoServidor = GetVistaActual().PedirModoDeJuego();
    }

    public void Jugar()
    {
        Inicio();

        if (_modoDeJuegoServidor)
        {
            GetVistaActual().MensajeEsperandoJugador2();
            _vistaJugador2 = new VistaSocket(8002);
            _vistaJugador2.MensajeBienvenida();
        }
        else {
            _vistaJugador2 = _vistaJugador1;
        }

        while (!EsFinJuego())
        {
            JugarMano();
        }
        DefinirGanador();
        FelicitarGanador();
        _vistaJugador1.Cerrar();
        _vistaJugador2.Cerrar();
    }

    private bool EsFinJuego()
    {
        return _jugadores.ExisteJugadorCon16Puntos();
    }

    private void JugarMano()
    {
        Mano mano = new Mano(_jugadores, _vistaJugador1, _vistaJugador2, _idJugadorTurno);
        mano.Jugar();
    }

    private void DefinirGanador()
    {
        //TODO: implementar
    }
    
    private void FelicitarGanador()
    {
        GetVistaActual().MostrarMensajeFelicitandoGanador(_jugadores.ObtenerJugador(_idGanador));
        //TODO: implementar       
    }
}