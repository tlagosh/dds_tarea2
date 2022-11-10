namespace EscobaServidor;

public class Mano
{
    private const int IdUsuario = 0;
    private const int CantidadInicialCartas = 3;

    private bool _modoDeJuegoServidor;

    private Jugadores _jugadores;
    private int _idJugadorTurno;

    private int _idUltimoJugadorQueTomoCarta;
    private CartasEnMesa _cartasEnMesa;

    private PilaCartas _pilaCartas = new PilaCartas();

    private Vista _vistaJugador1;
    private Vista _vistaJugador2;

    private Vista _vistaConsola;

    public Mano(Jugadores _jugadores, Vista _vistaJugador1, Vista vistaJugador2, Vista vistaConsola, bool modoDeJuegoServidor, int _idJugadorTurno)
    {
        this._jugadores = _jugadores;
        this._vistaJugador1 = _vistaJugador1;
        this._vistaJugador2 = vistaJugador2;
        this._vistaConsola = vistaConsola;
        this._modoDeJuegoServidor = modoDeJuegoServidor;
        this._idJugadorTurno = _idJugadorTurno;
        PonerMesaVacia();
        RepartirCartas();
        Poner4CartasEnMesa();
    }

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
    
    private void PonerMesaVacia() => _cartasEnMesa = new CartasEnMesa();
    private void RepartirCartas() => _jugadores.RepartirCartas(CantidadInicialCartas, _pilaCartas);

    private void Poner4CartasEnMesa()
    {
        for (int i = 0; i < 4; i++)
        {
            if (_pilaCartas.TieneCartas())
            {
                _cartasEnMesa.Agregar(_pilaCartas.SacarCartaAlAzar());
            }
        }
    }
    
        
    public void Jugar()
    {
        while (!EsFinMano())
        {
            JugarTurno();
        }
        foreach (Carta cartaEnMesa in _cartasEnMesa.Cartas)
        {
            _jugadores.ObtenerJugador(_idUltimoJugadorQueTomoCarta).AgregarCartaAGanadas(cartaEnMesa);
        }
        ContarPuntos();
    }

    private void ContarPuntos()
    {
        _jugadores.ContarPuntos();
    }

    private bool EsFinMano()
    {
        bool hayCartasEnPila = _pilaCartas.TieneCartas();
        bool AlguienTieneCartasEnMano = _jugadores.AlguienTieneCartasEnMano();
        return !_pilaCartas.TieneCartas() && !_jugadores.AlguienTieneCartasEnMano();
    }

    private void JugarTurno()
    {   
        if(!_jugadores.ObtenerJugador(_idJugadorTurno).TieneCartasEnMano())
        {
            if (_pilaCartas.CantidadCartas() >= CantidadInicialCartas)
            {
                _pilaCartas.DarCartas(_jugadores.ObtenerJugador(_idJugadorTurno), CantidadInicialCartas);
            }
            else {
                //stop function
                return;
            }
        }
        GetVistaActual().MostrarInfoJugador(_jugadores.ObtenerJugador(_idJugadorTurno));
        GetVistaActual().MostrarMesa(_cartasEnMesa);
        GetVistaActual().MostrarMano(_jugadores.ObtenerJugador(_idJugadorTurno));
        int cartaParaBajar = GetVistaActual().PedirCarta(_jugadores.ObtenerJugador(_idJugadorTurno));
        BajarCarta(cartaParaBajar);
        GetVistaActual().MostrarMesa(_cartasEnMesa);
        AvanzarTurno();
    }

    private bool BajarCarta(int cartaParaBajar)
    {
        Carta carta = _jugadores.ObtenerJugador(_idJugadorTurno).CartasMano[cartaParaBajar];
        _jugadores.ObtenerJugador(_idJugadorTurno).CartasMano.RemoveAt(cartaParaBajar);

        List<List<Carta>> jugadas = _cartasEnMesa.ObtenerTodasLasJugadas(carta);

        if (jugadas.Count == 0)
        {
            GetVistaActual().InformarQueNoExisteCombinacion(_jugadores.ObtenerJugador(_idJugadorTurno));
            _cartasEnMesa.Agregar(carta);
            return false;
        }
        else
        {
            int numeroDeJugada = GetVistaActual().PedirJugada(jugadas);
            foreach (Carta cartaGanada in jugadas[numeroDeJugada])
                _jugadores.ObtenerJugador(_idJugadorTurno).AgregarCartaAGanadas(cartaGanada);

            //Sacar las cartas de la mesa
            foreach (Carta cartaMesa in jugadas[numeroDeJugada])
                _cartasEnMesa.Sacar(cartaMesa);
            
            //Si la mesa queda vacía, es escoba
            if (_cartasEnMesa.CantidadCartas() == 0)
            {
                GetVistaActual().InformarEscoba(_jugadores.ObtenerJugador(_idJugadorTurno));
                _jugadores.ObtenerJugador(_idJugadorTurno).PuntosJuego += 1;
                _idUltimoJugadorQueTomoCarta = _idJugadorTurno;
                Poner4CartasEnMesa();

                return true;
            }
            else
            {
                GetVistaActual().MostrarMensajeCartasGanadas(_jugadores.ObtenerJugador(_idJugadorTurno), jugadas[numeroDeJugada]);
                _idUltimoJugadorQueTomoCarta = _idJugadorTurno;

                return false;
            }
        }
    }

    private void AvanzarTurno() => _idJugadorTurno = (_idJugadorTurno+1) % 2;
}