using System.Net;
using System.Net.Sockets;
namespace EscobaServidor;

public class Juego
{
    static private TcpListener _listener = new TcpListener(IPAddress.Any, 8001);
    private const int NumJugadores = 2;
    private const int CantidadInicialCartas = 3;

    private Jugadores _jugadores;
    private int _idJugadorTurno = 0;

    private int _idGanador = 0;
    private CartasEnMesa _cartasEnMesa;
    private Vista _vistaJugador1;
    private Vista _vistaJugador2;
    private Vista _vistaConsola = new VistaConsola();
    private bool _modoDeJuegoServidor = false;
    private Vista GetVistaActual() {
        if (!_modoDeJuegoServidor) {
            return _vistaConsola;
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
            _vistaConsola.MensajeEsperandoJugador1();
            _vistaJugador1 = new VistaSocket(_listener);
            _vistaJugador1.MensajeBienvenida();
            _vistaConsola.MensajeEsperandoJugador2();
            _vistaJugador2 = new VistaSocket(_listener);
            _vistaJugador2.MensajeBienvenida();
            _vistaConsola.MensajeJugadoresConectados();
        }

        while (!EsFinJuego())
        {
            JugarMano();
            if (_modoDeJuegoServidor)
            {
                _vistaJugador1.MostrarPuntosFinMano(_jugadores);
                _vistaJugador2.MostrarPuntosFinMano(_jugadores);
            }
            else {
                _vistaConsola.MostrarPuntosFinMano(_jugadores);
            }
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
        Mano mano = new Mano(_jugadores, _vistaJugador1, _vistaJugador2, _vistaConsola, _modoDeJuegoServidor, _idJugadorTurno);
        mano.Jugar();
    }

    private void DefinirGanador()
    {
        _idGanador = _jugadores.GetIdJugadorConMasPuntos();
    }
    
    private void FelicitarGanador()
    {
        _idJugadorTurno = _idGanador;
        GetVistaActual().MostrarMensajeFelicitandoGanador(_jugadores.ObtenerJugador(_idGanador));
        _idJugadorTurno = _idJugadorTurno == 0 ? 1 : 0;
        GetVistaActual().MostrarMensajePerdedor(_jugadores.ObtenerJugador(_idGanador), _jugadores.ObtenerJugador(_idJugadorTurno));
    }
}