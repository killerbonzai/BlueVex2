using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Windows.Forms;

namespace BlueVex.Core
{
    public class Proxy
    {
        #region Proxies

        internal BnetProxy BnetProxy { get; set; }

        private RealmProxy realmProxy;
        internal RealmProxy RealmProxy { 
            get 
            {
                return realmProxy;
            } 
            set 
            {
                realmProxy = value;
                RegisterRealmProxyEvents(this.realmProxy);
            }
        }

        private GameProxy gameProxy;
        internal GameProxy GameProxy
        {
            get
            {
                return gameProxy;
            }
            set
            {
                gameProxy = value;
                RegisterGameProxyEvents(this.gameProxy);
            }
        }

        private IDiabloWindow diabloWindow;
        public IDiabloWindow DiabloWindow
        {
            get
            {
                return diabloWindow;
            }
            set
            {
                diabloWindow = value;
                RegisterWindowEvents(this.diabloWindow);
            }
        }

        #endregion

        #region Bnet Proxy Events

        #region BattleNet to Diablo Events

        public event BlueVex.Core.BnetProxy.OnAdInfoEventHandler OnAdInfo = delegate { };
        public event BlueVex.Core.BnetProxy.OnBnetAuthResponseEventHandler OnBnetAuthResponse = delegate { };
        public event BlueVex.Core.BnetProxy.OnBnetConnectionResponseEventHandler OnBnetConnectionResponse = delegate { };
        public event BlueVex.Core.BnetProxy.OnBnetLogonResponseEventHandler OnBnetLogonResponse = delegate { };
        public event BlueVex.Core.BnetProxy.OnBnetPingEventHandler OnBnetPing = delegate { };
        public event BlueVex.Core.BnetProxy.OnChannelListEventHandler OnChannelList = delegate { };
        public event BlueVex.Core.BnetProxy.OnChatEventEventHandler OnChatEvent = delegate { };
        public event BlueVex.Core.BnetProxy.OnEnterChatResponseEventHandler OnEnterChatResponse = delegate { };
        public event BlueVex.Core.BnetProxy.OnExtraWorkInfoEventHandler OnExtraWorkInfo = delegate { };
        public event BlueVex.Core.BnetProxy.OnFileTimeInfoEventHandler OnFileTimeInfo = delegate { };
        public event BlueVex.Core.BnetProxy.OnNewsInfoEventHandler OnNewsInfo = delegate { };
        public event BlueVex.Core.BnetProxy.OnQueryRealmsResponseEventHandler OnQueryRealmsResponse = delegate { };
        public event BlueVex.Core.BnetProxy.OnRealmLogonResponseEventHandler OnRealmLogonResponse = delegate { };
        public event BlueVex.Core.BnetProxy.OnRequiredExtraWorkInfoEventHandler OnRequiredExtraWorkInfo = delegate { };
        public event BlueVex.Core.BnetProxy.OnServerKeepAliveEventHandler OnServerKeepAlive = delegate { };
        
        #endregion

        #region Diablo to BattleNet Events

        public event BlueVex.Core.BnetProxy.OnKeepAliveEventHandler OnKeepAlive = delegate { };
        public event BlueVex.Core.BnetProxy.OnEnterChatRequestEventHandler OnEnterChatRequest = delegate { };
        public event BlueVex.Core.BnetProxy.OnChannelListRequestEventHandler OnChannelListRequest = delegate { };
        public event BlueVex.Core.BnetProxy.OnJoinChannelEventHandler OnJoinChannel = delegate { };
        public event BlueVex.Core.BnetProxy.OnChatCommandEventHandler OnChatCommand = delegate { };
        public event BlueVex.Core.BnetProxy.OnLeaveChatEventHandler OnLeaveChat = delegate { };
        public event BlueVex.Core.BnetProxy.OnAdInfoRequestEventHandler OnAdInfoRequest = delegate { };
        public event BlueVex.Core.BnetProxy.OnStartGameEventHandler OnStartGame = delegate { };
        public event BlueVex.Core.BnetProxy.OnLeaveGameEventHandler OnLeaveGame = delegate { };
        public event BlueVex.Core.BnetProxy.OnDisplayAdEventHandler OnDisplayAd = delegate { };
        public event BlueVex.Core.BnetProxy.OnNotifyJoinEventHandler OnNotifyJoin = delegate { };
        public event BlueVex.Core.BnetProxy.OnBnetPongEventHandler OnBnetPong = delegate { };
        public event BlueVex.Core.BnetProxy.OnFileTimeRequestEventHandler OnFileTimeRequest = delegate { };
        public event BlueVex.Core.BnetProxy.OnBnetLogonRequestEventHandler OnBnetLogonRequest = delegate { };
        public event BlueVex.Core.BnetProxy.OnRealmLogonRequestEventHandler OnRealmLogonRequest = delegate { };
        public event BlueVex.Core.BnetProxy.OnQueryRealmsEventHandler OnQueryRealms = delegate { };
        public event BlueVex.Core.BnetProxy.OnNewsInfoRequestEventHandler OnNewsInfoRequest = delegate { };
        public event BlueVex.Core.BnetProxy.OnExtraWorkResponseEventHandler OnExtraWorkResponse = delegate { };
        public event BlueVex.Core.BnetProxy.OnBnetConnectionRequestEventHandler OnBnetConnectionRequest = delegate { };
        public event BlueVex.Core.BnetProxy.OnBnetAuthRequestEventHandler OnBnetAuthRequest = delegate { };
        
        #endregion

        #endregion

        #region Realm Proxy Events

        #region BattleNet to Diablo Events

        public event BlueVex.Core.RealmProxy.OnCharacterCreationResponseEventHandler OnCharacterCreationResponse = delegate { };
        public event BlueVex.Core.RealmProxy.OnCharacterDeletionResponseEventHandler OnCharacterDeletionResponse = delegate { };
        public event BlueVex.Core.RealmProxy.OnCharacterListEventHandler OnCharacterList = delegate { };
        public event BlueVex.Core.RealmProxy.OnCharacterLogonResponseEventHandler OnCharacterLogonResponse = delegate { };
        public event BlueVex.Core.RealmProxy.OnCharacterUpgradeResponseEventHandler OnCharacterUpgradeResponse = delegate { };
        public event BlueVex.Core.RealmProxy.OnCreateGameResponseEventHandler OnCreateGameResponse = delegate { };
        public event BlueVex.Core.RealmProxy.OnGameCreationQueueEventHandler OnGameCreationQueue = delegate { };
        public event BlueVex.Core.RealmProxy.OnGameInfoEventHandler OnGameInfo = delegate { };
        public event BlueVex.Core.RealmProxy.OnGameListEventHandler OnGameList = delegate { };
        public event BlueVex.Core.RealmProxy.OnJoinGameResponseEventHandler OnJoinGameResponse = delegate { };
        public event BlueVex.Core.RealmProxy.OnMessageOfTheDayEventHandler OnMessageOfTheDay = delegate { };
        public event BlueVex.Core.RealmProxy.OnRealmStartupResponseEventHandler OnRealmStartupResponse = delegate { };
        
        #endregion

        #region Diablo to BattleNet Events

        public event BlueVex.Core.RealmProxy.OnRealmStartupRequestEventHandler OnRealmStartupRequest = delegate { };
        public event BlueVex.Core.RealmProxy.OnCharacterCreationRequestEventHandler OnCharacterCreationRequest = delegate { };
        public event BlueVex.Core.RealmProxy.OnCreateGameRequestEventHandler OnCreateGameRequest = delegate { };
        public event BlueVex.Core.RealmProxy.OnJoinGameRequestEventHandler OnJoinGameRequest = delegate { };
        public event BlueVex.Core.RealmProxy.OnGameListRequestEventHandler OnGameListRequest = delegate { };
        public event BlueVex.Core.RealmProxy.OnGameInfoRequestEventHandler OnGameInfoRequest = delegate { };
        public event BlueVex.Core.RealmProxy.OnCharacterLogonRequestEventHandler OnCharacterLogonRequest = delegate { };
        public event BlueVex.Core.RealmProxy.OnCharacterDeletionRequestEventHandler OnCharacterDeletionRequest = delegate { };
        public event BlueVex.Core.RealmProxy.OnMessageOfTheDayRequestEventHandler OnMessageOfTheDayRequest = delegate { };
        public event BlueVex.Core.RealmProxy.OnCancelGameCreationEventHandler OnCancelGameCreation = delegate { };
        public event BlueVex.Core.RealmProxy.OnCharacterUpgradeRequestEventHandler OnCharacterUpgradeRequest = delegate { };
        public event BlueVex.Core.RealmProxy.OnCharacterListRequestEventHandler OnCharacterListRequest = delegate { };
        
        #endregion

        #endregion

        #region Game Proxy Events

        #region BattleNet to Diablo Events

        public event BlueVex.Core.GameProxy.OnAboutPlayerEventHandler OnAboutPlayer = delegate { };
        public event BlueVex.Core.GameProxy.OnAcceptTradeEventHandler OnAcceptTrade = delegate { };
        public event BlueVex.Core.GameProxy.OnAddUnitEventHandler OnAddUnit = delegate { };
        public event BlueVex.Core.GameProxy.OnAssignGameObjectEventHandler OnAssignGameObject = delegate { };
        public event BlueVex.Core.GameProxy.OnAssignMercEventHandler OnAssignMerc = delegate { };
        public event BlueVex.Core.GameProxy.OnAssignNPCEventHandler OnAssignNPC = delegate { };
        public event BlueVex.Core.GameProxy.OnAssignPlayerEventHandler OnAssignPlayer = delegate { };
        public event BlueVex.Core.GameProxy.OnAssignPlayerCorpseEventHandler OnAssignPlayerCorpse = delegate { };
        public event BlueVex.Core.GameProxy.OnAssignPlayerToPartyEventHandler OnAssignPlayerToParty = delegate { };
        public event BlueVex.Core.GameProxy.OnAssignSkillEventHandler OnAssignSkill = delegate { };
        public event BlueVex.Core.GameProxy.OnAssignSkillHotkeyEventHandler OnAssignSkillHotkey = delegate { };
        public event BlueVex.Core.GameProxy.OnAssignWarpEventHandler OnAssignWarp = delegate { };
        public event BlueVex.Core.GameProxy.OnAttributeNotificationEventHandler OnAttributeNotification = delegate { };
        public event BlueVex.Core.GameProxy.OnDelayedStateEventHandler OnDelayedState = delegate { };
        public event BlueVex.Core.GameProxy.OnEndStateEventHandler OnEndState = delegate { };
        public event BlueVex.Core.GameProxy.OnGainExperienceEventHandler OnGainExperience = delegate { };
        public event BlueVex.Core.GameProxy.OnGameHandshakeEventHandler OnGameHandshake = delegate { };
        public event BlueVex.Core.GameProxy.OnGameLoadingEventHandler OnGameLoading = delegate { };
        public event BlueVex.Core.GameProxy.OnGameLogonReceiptEventHandler OnGameLogonReceipt = delegate { };
        public event BlueVex.Core.GameProxy.OnGameLogonSuccessEventHandler OnGameLogonSuccess = delegate { };
        public event BlueVex.Core.GameProxy.OnGameLogoutSuccessEventHandler OnGameLogoutSuccess = delegate { };
        public event BlueVex.Core.GameProxy.OnGameOverEventHandler OnGameOver = delegate { };
        public event BlueVex.Core.GameProxy.OnGoldTradeEventHandler OnGoldTrade = delegate { };
        public event BlueVex.Core.GameProxy.OnInformationMessageEventHandler OnInformationMessage = delegate { };
        public event BlueVex.Core.GameProxy.OnItemTriggerSkillEventHandler OnItemTriggerSkill = delegate { };
        public event BlueVex.Core.GameProxy.OnLoadActEventHandler OnLoadAct = delegate { };
        public event BlueVex.Core.GameProxy.OnLoadDoneEventHandler OnLoadDone = delegate { };
        public event BlueVex.Core.GameProxy.OnMapAddEventHandler OnMapAdd = delegate { };
        public event BlueVex.Core.GameProxy.OnMapRemoveEventHandler OnMapRemove = delegate { };
        public event BlueVex.Core.GameProxy.OnMercAttributeNotificationEventHandler OnMercAttributeNotification = delegate { };
        public event BlueVex.Core.GameProxy.OnMercForHireEventHandler OnMercForHire = delegate { };
        public event BlueVex.Core.GameProxy.OnMercForHireListStartEventHandler OnMercForHireListStart = delegate { };
        public event BlueVex.Core.GameProxy.OnMercGainExperienceEventHandler OnMercGainExperience = delegate { };
        public event BlueVex.Core.GameProxy.OnMonsterAttackEventHandler OnMonsterAttack = delegate { };
        public event BlueVex.Core.GameProxy.OnNPCActionEventHandler OnNPCAction = delegate { };
        public event BlueVex.Core.GameProxy.OnNPCGetHitEventHandler OnNPCGetHit = delegate { };
        public event BlueVex.Core.GameProxy.OnNPCHealEventHandler OnNPCHeal = delegate { };
        public event BlueVex.Core.GameProxy.OnNPCInfoEventHandler OnNPCInfo = delegate { };
        public event BlueVex.Core.GameProxy.OnNPCMoveEventHandler OnNPCMove = delegate { };
        public event BlueVex.Core.GameProxy.OnNPCMoveToTargetEventHandler OnNPCMoveToTarget = delegate { };
        public event BlueVex.Core.GameProxy.OnNPCStopEventHandler OnNPCStop = delegate { };
        public event BlueVex.Core.GameProxy.OnNPCWantsInteractEventHandler OnNPCWantsInteract = delegate { };
        public event BlueVex.Core.GameProxy.OnOpenWaypointEventHandler OnOpenWaypoint = delegate { };
        public event BlueVex.Core.GameProxy.OnOwnedItemActionEventHandler OnOwnedItemAction = delegate { };
        public event BlueVex.Core.GameProxy.OnPartyMemberPulseEventHandler OnPartyMemberPulse = delegate { };
        public event BlueVex.Core.GameProxy.OnPartyMemberUpdateEventHandler OnPartyMemberUpdate = delegate { };
        public event BlueVex.Core.GameProxy.OnPartyRefreshEventHandler OnPartyRefresh = delegate { };
        public event BlueVex.Core.GameProxy.OnPlayerAttributeNotificationEventHandler OnPlayerAttributeNotification = delegate { };
        public event BlueVex.Core.GameProxy.OnPlayerClearCursorEventHandler OnPlayerClearCursor = delegate { };
        public event BlueVex.Core.GameProxy.OnPlayerCorpseVisibleEventHandler OnPlayerCorpseVisible = delegate { };
        public event BlueVex.Core.GameProxy.OnPlayerInGameEventHandler OnPlayerInGame = delegate { };
        public event BlueVex.Core.GameProxy.OnPlayerInSightEventHandler OnPlayerInSight = delegate { };
        public event BlueVex.Core.GameProxy.OnPlayerKillCountEventHandler OnPlayerKillCount = delegate { };
        public event BlueVex.Core.GameProxy.OnPlayerLeaveGameEventHandler OnPlayerLeaveGame = delegate { };
        public event BlueVex.Core.GameProxy.OnPlayerLifeManaChangeEventHandler OnPlayerLifeManaChange = delegate { };
        public event BlueVex.Core.GameProxy.OnPlayerMoveEventHandler OnPlayerMove = delegate { };
        public event BlueVex.Core.GameProxy.OnPlayerMoveToTargetEventHandler OnPlayerMoveToTarget = delegate { };
        public event BlueVex.Core.GameProxy.OnPlayerPartyRelationshipEventHandler OnPlayerPartyRelationship = delegate { };
        public event BlueVex.Core.GameProxy.OnPlayerReassignEventHandler OnPlayerReassign = delegate { };
        public event BlueVex.Core.GameProxy.OnPlayerRelationshipEventHandler OnPlayerRelationship = delegate { };
        public event BlueVex.Core.GameProxy.OnPlayerStopEventHandler OnPlayerStop = delegate { };
        public event BlueVex.Core.GameProxy.OnPlaySoundEventHandler OnPlaySound = delegate { };
        public event BlueVex.Core.GameProxy.OnPongEventHandler OnPong = delegate { };
        public event BlueVex.Core.GameProxy.OnPortalInfoEventHandler OnPortalInfo = delegate { };
        public event BlueVex.Core.GameProxy.OnPortalOwnershipEventHandler OnPortalOwnership = delegate { };
        public event BlueVex.Core.GameProxy.OnQuestItemStateEventHandler OnQuestItemState = delegate { };
        public event BlueVex.Core.GameProxy.OnReceiveMessageEventHandler OnReceiveMessage = delegate { };
        public event BlueVex.Core.GameProxy.OnRelator1EventHandler OnRelator1 = delegate { };
        public event BlueVex.Core.GameProxy.OnRelator2EventHandler OnRelator2 = delegate { };
        public event BlueVex.Core.GameProxy.OnRemoveGroundUnitEventHandler OnRemoveGroundUnit = delegate { };
        public event BlueVex.Core.GameProxy.OnReportKillEventHandler OnReportKill = delegate { };
        public event BlueVex.Core.GameProxy.OnRequestLogonInfoEventHandler OnRequestLogonInfo = delegate { };
        public event BlueVex.Core.GameProxy.OnSetGameObjectModeEventHandler OnSetGameObjectMode = delegate { };
        public event BlueVex.Core.GameProxy.OnSetItemStateEventHandler OnSetItemState = delegate { };
        public event BlueVex.Core.GameProxy.OnSetNPCModeEventHandler OnSetNPCMode = delegate { };
        public event BlueVex.Core.GameProxy.OnSetStateEventHandler OnSetState = delegate { };
        public event BlueVex.Core.GameProxy.OnSkillsLogEventHandler OnSkillsLog = delegate { };
        public event BlueVex.Core.GameProxy.OnSmallGoldAddEventHandler OnSmallGoldAdd = delegate { };
        public event BlueVex.Core.GameProxy.OnSummonActionEventHandler OnSummonAction = delegate { };
        public event BlueVex.Core.GameProxy.OnSwitchWeaponSetEventHandler OnSwitchWeaponSet = delegate { };
        public event BlueVex.Core.GameProxy.OnTransactionCompleteEventHandler OnTransactionComplete = delegate { };
        public event BlueVex.Core.GameProxy.OnUnitUseSkillEventHandler OnUnitUseSkill = delegate { };
        public event BlueVex.Core.GameProxy.OnUnitUseSkillOnTargetEventHandler OnUnitUseSkillOnTarget = delegate { };
        public event BlueVex.Core.GameProxy.OnUnloadDoneEventHandler OnUnloadDone = delegate { };
        public event BlueVex.Core.GameProxy.OnUpdateGameQuestLogEventHandler OnUpdateGameQuestLog = delegate { };
        public event BlueVex.Core.GameProxy.OnUpdateItemStatsEventHandler OnUpdateItemStats = delegate { };
        public event BlueVex.Core.GameProxy.OnUpdateItemUIEventHandler OnUpdateItemUI = delegate { };
        public event BlueVex.Core.GameProxy.OnUpdatePlayerItemSkillEventHandler OnUpdatePlayerItemSkill = delegate { };
        public event BlueVex.Core.GameProxy.OnUpdateQuestInfoEventHandler OnUpdateQuestInfo = delegate { };
        public event BlueVex.Core.GameProxy.OnUpdateQuestLogEventHandler OnUpdateQuestLog = delegate { };
        public event BlueVex.Core.GameProxy.OnUpdateSkillEventHandler OnUpdateSkill = delegate { };
        public event BlueVex.Core.GameProxy.OnUseSpecialItemEventHandler OnUseSpecialItem = delegate { };
        public event BlueVex.Core.GameProxy.OnUseStackableItemEventHandler OnUseStackableItem = delegate { };
        public event BlueVex.Core.GameProxy.OnWalkVerifyEventHandler OnWalkVerify = delegate { };
        public event BlueVex.Core.GameProxy.OnWardenCheckEventHandler OnWardenCheck = delegate { };
        public event BlueVex.Core.GameProxy.OnWorldItemActionEventHandler OnWorldItemAction = delegate { };
        
        #endregion

        #region Diablo to Battle Net Events

        public event BlueVex.Core.GameProxy.OnGameLogonRequestEventHandler OnGameLogonRequest = delegate { };
        public event BlueVex.Core.GameProxy.OnWalkToLocationEventHandler OnWalkToLocation = delegate { };
        public event BlueVex.Core.GameProxy.OnWalkToTargetEventHandler OnWalkToTarget = delegate { };
        public event BlueVex.Core.GameProxy.OnRunToLocationEventHandler OnRunToLocation = delegate { };
        public event BlueVex.Core.GameProxy.OnRunToTargetEventHandler OnRunToTarget = delegate { };
        public event BlueVex.Core.GameProxy.OnCastLeftSkillEventHandler OnCastLeftSkill = delegate { };
        public event BlueVex.Core.GameProxy.OnCastLeftSkillOnTargetEventHandler OnCastLeftSkillOnTarget = delegate { };
        public event BlueVex.Core.GameProxy.OnCastLeftSkillOnTargetStoppedEventHandler OnCastLeftSkillOnTargetStopped = delegate { };
        public event BlueVex.Core.GameProxy.OnRecastLeftSkillEventHandler OnRecastLeftSkill = delegate { };
        public event BlueVex.Core.GameProxy.OnRecastLeftSkillOnTargetEventHandler OnRecastLeftSkillOnTarget = delegate { };
        public event BlueVex.Core.GameProxy.OnRecastLeftSkillOnTargetStoppedEventHandler OnRecastLeftSkillOnTargetStopped = delegate { };
        public event BlueVex.Core.GameProxy.OnCastRightSkillEventHandler OnCastRightSkill = delegate { };
        public event BlueVex.Core.GameProxy.OnCastRightSkillOnTargetEventHandler OnCastRightSkillOnTarget = delegate { };
        public event BlueVex.Core.GameProxy.OnCastRightSkillOnTargetStoppedEventHandler OnCastRightSkillOnTargetStopped = delegate { };
        public event BlueVex.Core.GameProxy.OnRecastRightSkillEventHandler OnRecastRightSkill = delegate { };
        public event BlueVex.Core.GameProxy.OnRecastRightSkillOnTargetEventHandler OnRecastRightSkillOnTarget = delegate { };
        public event BlueVex.Core.GameProxy.OnRecastRightSkillOnTargetStoppedEventHandler OnRecastRightSkillOnTargetStopped = delegate { };
        public event BlueVex.Core.GameProxy.OnUnitInteractEventHandler OnUnitInteract = delegate { };
        public event BlueVex.Core.GameProxy.OnSendOverheadMessageEventHandler OnSendOverheadMessage = delegate { };
        public event BlueVex.Core.GameProxy.OnSendMessageEventHandler OnSendMessage = delegate { };
        public event BlueVex.Core.GameProxy.OnPickItemEventHandler OnPickItem = delegate { };
        public event BlueVex.Core.GameProxy.OnDropItemEventHandler OnDropItem = delegate { };
        public event BlueVex.Core.GameProxy.OnDropItemToContainerEventHandler OnDropItemToContainer = delegate { };
        public event BlueVex.Core.GameProxy.OnPickItemFromContainerEventHandler OnPickItemFromContainer = delegate { };
        public event BlueVex.Core.GameProxy.OnEquipItemEventHandler OnEquipItem = delegate { };
        public event BlueVex.Core.GameProxy.OnUnequipItemEventHandler OnUnequipItem = delegate { };
        public event BlueVex.Core.GameProxy.OnSwapEquippedItemEventHandler OnSwapEquippedItem = delegate { };
        public event BlueVex.Core.GameProxy.OnSwapContainerItemEventHandler OnSwapContainerItem = delegate { };
        public event BlueVex.Core.GameProxy.OnUseInventoryItemEventHandler OnUseInventoryItem = delegate { };
        public event BlueVex.Core.GameProxy.OnStackItemsEventHandler OnStackItems = delegate { };
        public event BlueVex.Core.GameProxy.OnAddBeltItemEventHandler OnAddBeltItem = delegate { };
        public event BlueVex.Core.GameProxy.OnRemoveBeltItemEventHandler OnRemoveBeltItem = delegate { };
        public event BlueVex.Core.GameProxy.OnSwapBeltItemEventHandler OnSwapBeltItem = delegate { };
        public event BlueVex.Core.GameProxy.OnUseBeltItemEventHandler OnUseBeltItem = delegate { };
        public event BlueVex.Core.GameProxy.OnIdentifyItemEventHandler OnIdentifyItem = delegate { };
        public event BlueVex.Core.GameProxy.OnEmbedItemEventHandler OnEmbedItem = delegate { };
        public event BlueVex.Core.GameProxy.OnItemToCubeEventHandler OnItemToCube = delegate { };
        public event BlueVex.Core.GameProxy.OnTownFolkInteractEventHandler OnTownFolkInteract = delegate { };
        public event BlueVex.Core.GameProxy.OnTownFolkCancelInteractionEventHandler OnTownFolkCancelInteraction = delegate { };
        public event BlueVex.Core.GameProxy.OnDisplayQuestMessageEventHandler OnDisplayQuestMessage = delegate { };
        public event BlueVex.Core.GameProxy.OnBuyItemEventHandler OnBuyItem = delegate { };
        public event BlueVex.Core.GameProxy.OnSellItemEventHandler OnSellItem = delegate { };
        public event BlueVex.Core.GameProxy.OnCainIdentifyItemsEventHandler OnCainIdentifyItems = delegate { };
        public event BlueVex.Core.GameProxy.OnTownFolkRepairEventHandler OnTownFolkRepair = delegate { };
        public event BlueVex.Core.GameProxy.OnHireMercenaryEventHandler OnHireMercenary = delegate { };
        public event BlueVex.Core.GameProxy.OnIdentifyGambleItemEventHandler OnIdentifyGambleItem = delegate { };
        public event BlueVex.Core.GameProxy.OnTownFolkMenuSelectEventHandler OnTownFolkMenuSelect = delegate { };
        public event BlueVex.Core.GameProxy.OnIncrementAttributeEventHandler OnIncrementAttribute = delegate { };
        public event BlueVex.Core.GameProxy.OnIncrementSkillEventHandler OnIncrementSkill = delegate { };
        public event BlueVex.Core.GameProxy.OnSelectSkillEventHandler OnSelectSkill = delegate { };
        public event BlueVex.Core.GameProxy.OnHoverUnitEventHandler OnHoverUnit = delegate { };
        public event BlueVex.Core.GameProxy.OnSendCharacterSpeechEventHandler OnSendCharacterSpeech = delegate { };
        public event BlueVex.Core.GameProxy.OnRequestQuestLogEventHandler OnRequestQuestLog = delegate { };
        public event BlueVex.Core.GameProxy.OnRespawnEventHandler OnRespawn = delegate { };
        public event BlueVex.Core.GameProxy.OnWaypointInteractEventHandler OnWaypointInteract = delegate { };
        public event BlueVex.Core.GameProxy.OnRequestReassignEventHandler OnRequestReassign = delegate { };
        public event BlueVex.Core.GameProxy.OnClickButtonEventHandler OnClickButton = delegate { };
        public event BlueVex.Core.GameProxy.OnDropGoldEventHandler OnDropGold = delegate { };
        public event BlueVex.Core.GameProxy.OnSetSkillHotkeyEventHandler OnSetSkillHotkey = delegate { };
        public event BlueVex.Core.GameProxy.OnCloseQuestEventHandler OnCloseQuest = delegate { };
        public event BlueVex.Core.GameProxy.OnGoToTownFolkEventHandler OnGoToTownFolk = delegate { };
        public event BlueVex.Core.GameProxy.OnSetPlayerRelationEventHandler OnSetPlayerRelation = delegate { };
        public event BlueVex.Core.GameProxy.OnPartyRequestEventHandler OnPartyRequest = delegate { };
        public event BlueVex.Core.GameProxy.OnUpdatePositionEventHandler OnUpdatePosition = delegate { };
        public event BlueVex.Core.GameProxy.OnSwitchWeaponsEventHandler OnSwitchWeapons = delegate { };
        public event BlueVex.Core.GameProxy.OnChangeMercEquipmentEventHandler OnChangeMercEquipment = delegate { };
        public event BlueVex.Core.GameProxy.OnResurrectMercEventHandler OnResurrectMerc = delegate { };
        public event BlueVex.Core.GameProxy.OnInventoryItemToBeltEventHandler OnInventoryItemToBelt = delegate { };
        public event BlueVex.Core.GameProxy.OnWardenResponseEventHandler OnWardenResponse = delegate { };
        public event BlueVex.Core.GameProxy.OnExitGameEventHandler OnExitGame = delegate { };
        public event BlueVex.Core.GameProxy.OnEnterGameEventHandler OnEnterGame = delegate { };
        public event BlueVex.Core.GameProxy.OnPingEventHandler OnPing = delegate { };
        public event BlueVex.Core.GameProxy.OnGoToLocationEventHandler OnGoToLocation = delegate { };
        public event BlueVex.Core.GameProxy.OnGoToTargetEventHandler OnGoToTarget = delegate { };
        
        #endregion

        #endregion

        #region Window Events

        public event KeyEventHandler KeyDown;
        public event KeyEventHandler KeyUp;

        #endregion

        #region Register Bnet Events

        //TODO: Add code here

        #endregion

        #region Register Realm Events

        private void RegisterRealmProxyEvents(RealmProxy proxy)
        {
            proxy.OnJoinGameRequest += this.OnJoinGameRequest;
            proxy.OnJoinGameResponse += this.OnJoinGameResponse;

            //TODO: Add the rest of the events
        }

        #endregion

        #region Register Game Events

        private void RegisterGameProxyEvents(GameProxy proxy)
        {
            // Bnet Events
            proxy.OnAboutPlayer += this.OnAboutPlayer;
            proxy.OnAcceptTrade += this.OnAcceptTrade;
            proxy.OnAddUnit += this.OnAddUnit;
            proxy.OnAssignGameObject += this.OnAssignGameObject;
            proxy.OnAssignMerc += this.OnAssignMerc;
            proxy.OnAssignNPC += this.OnAssignNPC;
            proxy.OnAssignPlayer += this.OnAssignPlayer;
            proxy.OnAssignPlayerCorpse += this.OnAssignPlayerCorpse;
            proxy.OnAssignPlayerToParty += this.OnAssignPlayerToParty;
            proxy.OnAssignSkill += this.OnAssignSkill;
            proxy.OnAssignSkillHotkey += this.OnAssignSkillHotkey;
            proxy.OnAssignWarp += this.OnAssignWarp;
            proxy.OnAttributeNotification += this.OnAttributeNotification;
            proxy.OnDelayedState += this.OnDelayedState;
            proxy.OnEndState += this.OnEndState;
            proxy.OnGainExperience += this.OnGainExperience;
            proxy.OnGameHandshake += this.OnGameHandshake;
            proxy.OnGameLoading += this.OnGameLoading;
            proxy.OnGameLogonReceipt += this.OnGameLogonReceipt;
            proxy.OnGameLogonSuccess += this.OnGameLogonSuccess;
            proxy.OnGameLogoutSuccess += this.OnGameLogoutSuccess;
            proxy.OnGameOver += this.OnGameOver;
            proxy.OnGoldTrade += this.OnGoldTrade;
            proxy.OnInformationMessage += this.OnInformationMessage;
            proxy.OnItemTriggerSkill += this.OnItemTriggerSkill;
            proxy.OnLoadAct += this.OnLoadAct;
            proxy.OnLoadDone += this.OnLoadDone;
            proxy.OnMapAdd += this.OnMapAdd;
            proxy.OnMapRemove += this.OnMapRemove;
            proxy.OnMercAttributeNotification += this.OnMercAttributeNotification;
            proxy.OnMercForHire += this.OnMercForHire;
            proxy.OnMercForHireListStart += this.OnMercForHireListStart;
            proxy.OnMercGainExperience += this.OnMercGainExperience;
            proxy.OnMonsterAttack += this.OnMonsterAttack;
            proxy.OnNPCAction += this.OnNPCAction;
            proxy.OnNPCGetHit += this.OnNPCGetHit;
            proxy.OnNPCHeal += this.OnNPCHeal;
            proxy.OnNPCInfo += this.OnNPCInfo;
            proxy.OnNPCMove += this.OnNPCMove;
            proxy.OnNPCMoveToTarget += this.OnNPCMoveToTarget;
            proxy.OnNPCStop += this.OnNPCStop;
            proxy.OnNPCWantsInteract += this.OnNPCWantsInteract;
            proxy.OnOpenWaypoint += this.OnOpenWaypoint;
            proxy.OnOwnedItemAction += this.OnOwnedItemAction;
            proxy.OnPartyMemberPulse += this.OnPartyMemberPulse;
            proxy.OnPartyMemberUpdate += this.OnPartyMemberUpdate;
            proxy.OnPartyRefresh  += this.OnPartyRefresh;
            proxy.OnPlayerAttributeNotification += this.OnPlayerAttributeNotification;
            proxy.OnPlayerClearCursor += this.OnPlayerClearCursor;
            proxy.OnPlayerCorpseVisible += this.OnPlayerCorpseVisible;
            proxy.OnPlayerInGame += this.OnPlayerInGame;
            proxy.OnPlayerInSight += this.OnPlayerInSight;
            proxy.OnPlayerKillCount += this.OnPlayerKillCount;
            proxy.OnPlayerLeaveGame += this.OnPlayerLeaveGame;
            proxy.OnPlayerLifeManaChange += this.OnPlayerLifeManaChange;
            proxy.OnPlayerMove += this.OnPlayerMove;
            proxy.OnPlayerMoveToTarget += this.OnPlayerMoveToTarget;
            proxy.OnPlayerPartyRelationship += this.OnPlayerPartyRelationship;
            proxy.OnPlayerReassign += this.OnPlayerReassign;
            proxy.OnPlayerRelationship += this.OnPlayerRelationship;
            proxy.OnPlayerStop += this.OnPlayerStop;
            proxy.OnPlaySound += this.OnPlaySound;
            proxy.OnPong += this.OnPong;
            proxy.OnPortalInfo += this.OnPortalInfo;
            proxy.OnPortalOwnership += this.OnPortalOwnership;
            proxy.OnQuestItemState += this.OnQuestItemState;
            proxy.OnReceiveMessage += this.OnReceiveMessage;
            proxy.OnRelator1 += this.OnRelator1;
            proxy.OnRelator2 += this.OnRelator2;
            proxy.OnRemoveGroundUnit += this.OnRemoveGroundUnit;
            proxy.OnReportKill += this.OnReportKill;
            proxy.OnRequestLogonInfo += this.OnRequestLogonInfo;
            proxy.OnSetGameObjectMode += this.OnSetGameObjectMode;
            proxy.OnSetItemState += this.OnSetItemState;
            proxy.OnSetNPCMode += this.OnSetNPCMode;
            proxy.OnSetState += this.OnSetState;
            proxy.OnSkillsLog += this.OnSkillsLog;
            proxy.OnSmallGoldAdd += this.OnSmallGoldAdd;
            proxy.OnSummonAction += this.OnSummonAction;
            proxy.OnSwitchWeaponSet += this.OnSwitchWeaponSet;
            proxy.OnTransactionComplete += this.OnTransactionComplete;
            proxy.OnUnitUseSkill += this.OnUnitUseSkill;
            proxy.OnUnitUseSkillOnTarget += this.OnUnitUseSkillOnTarget;
            proxy.OnUnloadDone += this.OnUnloadDone;
            proxy.OnUpdateGameQuestLog += this.OnUpdateGameQuestLog;
            proxy.OnUpdateItemStats += this.OnUpdateItemStats;
            proxy.OnUpdateItemUI += this.OnUpdateItemUI;
            proxy.OnUpdatePlayerItemSkill += this.OnUpdatePlayerItemSkill;
            proxy.OnUpdateQuestInfo += this.OnUpdateQuestInfo;
            proxy.OnUpdateQuestLog += this.OnUpdateQuestLog;
            proxy.OnUpdateSkill += this.OnUpdateSkill;
            proxy.OnUseSpecialItem += this.OnUseSpecialItem;
            proxy.OnUseStackableItem += this.OnUseStackableItem;
            proxy.OnWalkVerify += this.OnWalkVerify;
            proxy.OnWardenCheck += this.OnWardenCheck;
            proxy.OnWorldItemAction += this.OnWorldItemAction;

            //TODO: Add Diablo events here
            proxy.OnExitGame += this.OnExitGame;
        }

        public void CompressSendToDiablo(byte[] data)
        {
            if (this.gameProxy != null)
            {
                this.gameProxy.CompressSendToDiablo(data);
            }
        }

        #endregion

        #region Register Window Events

        private void RegisterWindowEvents(IDiabloWindow window)
        {
            window.KeyDown += this.KeyDown;
            window.KeyUp += this.KeyUp;
        }

        #endregion
    }
}
