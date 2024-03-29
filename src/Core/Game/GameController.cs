﻿using System;
using Amplitude.Mercury.AI;
using Amplitude.Mercury.Sandbox;

namespace Modding.Humankind.DevTools.Core
{
    internal class GameController : GameEventController
    {
        public static bool IsNewGame;

        private static GameController _instance;
        private static bool _isReady;
        private static string _gameId;
        private static bool _hasPendingInvoke;

        private GameController()
        {
            _isReady = false;
            _gameId = null;

            SynchronizationRate = 0.2f;
        }

        public static int CurrentTurn { get; private set; }

        public static HumankindEmpire[] GameEmpires { get; private set; }

        public static bool IsGameLoaded { get; private set; }

        public static bool IsReady => _isReady;

        public static float SynchronizationRate { get; private set; }

        // Used to initialize Instance sooner.
        public float SyncRate => SynchronizationRate;
        
        public static int GameID { get; private set; }

        public static GameController Instance
        {
            get
            {
                if (_instance == null) _instance = new GameController();

                return _instance;
            }
        }


        public static bool IsDirty { get; private set; }

        public static void MakeDirty()
        {
            IsDirty = true;
            SynchronizationRate = 0.2f;
            IsGameLoaded = false;
        }

        public static void SynchronizeGameState()
        {
            if (_hasPendingInvoke && (SandboxManager.IsStarted && Amplitude.Mercury.Presentation.Presentation.HasBeenStarted))
                if (GameUtils.IsInValidGameState(Sandbox.AIController))
                {
                    _hasPendingInvoke = false;

                    // Register actions found in decoupled modules annotated with [DevToolsModule] attribute.
                    ReloadAllModules(false);
                    
                    Instance.Setup((int) R.Fields.LastRunTurnField.GetValue(Sandbox.AIController), Sandbox.AIController,
                        Sandbox.GameID, true);

                    InvokeOnGameHasLoaded();

                    SynchronizationRate = 1.0f;
                }

            if (!_hasPendingInvoke && IsGameLoaded != (SandboxManager.IsStarted && Amplitude.Mercury.Presentation.Presentation.HasBeenStarted))
            {
                IsGameLoaded = !IsGameLoaded;

                if (IsGameLoaded)
                    _hasPendingInvoke = true;
                else
                    Unload();
            }
        }

        public void Setup(int currentTurn, AIController aiController, string gameId, bool isFirstRun)
        {
            CurrentTurn = currentTurn;

            if (_gameId != gameId)
                _isReady = false;

            if (!_isReady || _isReady && IsDirty)
            {
                _gameId = gameId;
                GameEmpires = GameUtils.GetGameEmpires(aiController);
                OnGameLoaded();
                IsDirty = false;
                _isReady = true;
            }

            if (!isFirstRun)
            {
                InvokeOnNewTurnStart();
            }
        }
        
        // Called ONCE everytime a user loads a different saved game or starts a new game.
        private void OnGameLoaded()
        {
            // Compute base values generated from Sandbox.GameID that will always be the same for that GameID
            GameID = Math.Abs(_gameId.GetHashCode());
        }

        public static void Unload()
        {
            InvokeOnGameHasUnloaded();
            ModuleHelper.Reset();

            _hasPendingInvoke = false;
            IsGameLoaded = false;
            IsNewGame = false;

            GameEmpires = null;
            CurrentTurn = 0;

            _instance = null;
        }
    }
}