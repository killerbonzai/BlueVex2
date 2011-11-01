using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using D2Packets;
using D2Compression;

namespace BlueVex.Core
{
    public class GameProxy : BaseProxy
    {

        #region BattleNet to Diablo Events

        public event OnAboutPlayerEventHandler OnAboutPlayer = delegate { };
        public delegate void OnAboutPlayerEventHandler(GameServer.AboutPlayer Packet, ref PacketFlag flag);
        public event OnAcceptTradeEventHandler OnAcceptTrade = delegate { };
        public delegate void OnAcceptTradeEventHandler(GameServer.AcceptTrade Packet, ref PacketFlag flag);
        public event OnAddUnitEventHandler OnAddUnit = delegate { };
        public delegate void OnAddUnitEventHandler(GameServer.AddUnit Packet, ref PacketFlag flag);
        public event OnAssignGameObjectEventHandler OnAssignGameObject = delegate { };
        public delegate void OnAssignGameObjectEventHandler(GameServer.AssignGameObject Packet, ref PacketFlag flag);
        public event OnAssignMercEventHandler OnAssignMerc = delegate { };
        public delegate void OnAssignMercEventHandler(GameServer.AssignMerc Packet, ref PacketFlag flag);
        public event OnAssignNPCEventHandler OnAssignNPC = delegate { };
        public delegate void OnAssignNPCEventHandler(GameServer.AssignNPC Packet, ref PacketFlag flag);
        public event OnAssignPlayerEventHandler OnAssignPlayer = delegate { };
        public delegate void OnAssignPlayerEventHandler(GameServer.AssignPlayer Packet, ref PacketFlag flag);
        public event OnAssignPlayerCorpseEventHandler OnAssignPlayerCorpse = delegate { };
        public delegate void OnAssignPlayerCorpseEventHandler(GameServer.AssignPlayerCorpse Packet, ref PacketFlag flag);
        public event OnAssignPlayerToPartyEventHandler OnAssignPlayerToParty = delegate { };
        public delegate void OnAssignPlayerToPartyEventHandler(GameServer.AssignPlayerToParty Packet, ref PacketFlag flag);
        public event OnAssignSkillEventHandler OnAssignSkill = delegate { };
        public delegate void OnAssignSkillEventHandler(GameServer.AssignSkill Packet, ref PacketFlag flag);
        public event OnAssignSkillHotkeyEventHandler OnAssignSkillHotkey = delegate { };
        public delegate void OnAssignSkillHotkeyEventHandler(GameServer.AssignSkillHotkey Packet, ref PacketFlag flag);
        public event OnAssignWarpEventHandler OnAssignWarp = delegate { };
        public delegate void OnAssignWarpEventHandler(GameServer.AssignWarp Packet, ref PacketFlag flag);
        public event OnAttributeNotificationEventHandler OnAttributeNotification = delegate { };
        public delegate void OnAttributeNotificationEventHandler(GameServer.AttributeNotification Packet, ref PacketFlag flag);
        public event OnDelayedStateEventHandler OnDelayedState = delegate { };
        public delegate void OnDelayedStateEventHandler(GameServer.DelayedState Packet, ref PacketFlag flag);
        public event OnEndStateEventHandler OnEndState = delegate { };
        public delegate void OnEndStateEventHandler(GameServer.EndState Packet, ref PacketFlag flag);
        public event OnGainExperienceEventHandler OnGainExperience = delegate { };
        public delegate void OnGainExperienceEventHandler(GameServer.GainExperience Packet, ref PacketFlag flag);
        public event OnGameHandshakeEventHandler OnGameHandshake = delegate { };
        public delegate void OnGameHandshakeEventHandler(GameServer.GameHandshake Packet, ref PacketFlag flag);
        public event OnGameLoadingEventHandler OnGameLoading = delegate { };
        public delegate void OnGameLoadingEventHandler(GameServer.GameLoading Packet, ref PacketFlag flag);
        public event OnGameLogonReceiptEventHandler OnGameLogonReceipt = delegate { };
        public delegate void OnGameLogonReceiptEventHandler(GameServer.GameLogonReceipt Packet, ref PacketFlag flag);
        public event OnGameLogonSuccessEventHandler OnGameLogonSuccess = delegate { };
        public delegate void OnGameLogonSuccessEventHandler(GameServer.GameLogonSuccess Packet, ref PacketFlag flag);
        public event OnGameLogoutSuccessEventHandler OnGameLogoutSuccess = delegate { };
        public delegate void OnGameLogoutSuccessEventHandler(GameServer.GameLogoutSuccess Packet, ref PacketFlag flag);
        public event OnGameOverEventHandler OnGameOver = delegate { };
        public delegate void OnGameOverEventHandler(GameServer.GameOver Packet, ref PacketFlag flag);
        public event OnGoldTradeEventHandler OnGoldTrade = delegate { };
        public delegate void OnGoldTradeEventHandler(GameServer.GoldTrade Packet, ref PacketFlag flag);
        public event OnInformationMessageEventHandler OnInformationMessage = delegate { };
        public delegate void OnInformationMessageEventHandler(GameServer.InformationMessage Packet, ref PacketFlag flag);
        public event OnItemTriggerSkillEventHandler OnItemTriggerSkill = delegate { };
        public delegate void OnItemTriggerSkillEventHandler(GameServer.ItemTriggerSkill Packet, ref PacketFlag flag);
        public event OnLoadActEventHandler OnLoadAct = delegate { };
        public delegate void OnLoadActEventHandler(GameServer.LoadAct Packet, ref PacketFlag flag);
        public event OnLoadDoneEventHandler OnLoadDone = delegate { };
        public delegate void OnLoadDoneEventHandler(GameServer.LoadDone Packet, ref PacketFlag flag);
        public event OnMapAddEventHandler OnMapAdd = delegate { };
        public delegate void OnMapAddEventHandler(GameServer.MapAdd Packet, ref PacketFlag flag);
        public event OnMapRemoveEventHandler OnMapRemove = delegate { };
        public delegate void OnMapRemoveEventHandler(GameServer.MapRemove Packet, ref PacketFlag flag);
        public event OnMercAttributeNotificationEventHandler OnMercAttributeNotification = delegate { };
        public delegate void OnMercAttributeNotificationEventHandler(GameServer.MercAttributeNotification Packet, ref PacketFlag flag);
        public event OnMercForHireEventHandler OnMercForHire = delegate { };
        public delegate void OnMercForHireEventHandler(GameServer.MercForHire Packet, ref PacketFlag flag);
        public event OnMercForHireListStartEventHandler OnMercForHireListStart = delegate { };
        public delegate void OnMercForHireListStartEventHandler(GameServer.MercForHireListStart Packet, ref PacketFlag flag);
        public event OnMercGainExperienceEventHandler OnMercGainExperience = delegate { };
        public delegate void OnMercGainExperienceEventHandler(GameServer.MercGainExperience Packet, ref PacketFlag flag);
        public event OnMonsterAttackEventHandler OnMonsterAttack = delegate { };
        public delegate void OnMonsterAttackEventHandler(GameServer.MonsterAttack Packet, ref PacketFlag flag);
        public event OnNPCActionEventHandler OnNPCAction = delegate { };
        public delegate void OnNPCActionEventHandler(GameServer.NPCAction Packet, ref PacketFlag flag);
        public event OnNPCGetHitEventHandler OnNPCGetHit = delegate { };
        public delegate void OnNPCGetHitEventHandler(GameServer.NPCGetHit Packet, ref PacketFlag flag);
        public event OnNPCHealEventHandler OnNPCHeal = delegate { };
        public delegate void OnNPCHealEventHandler(GameServer.NPCHeal Packet, ref PacketFlag flag);
        public event OnNPCInfoEventHandler OnNPCInfo = delegate { };
        public delegate void OnNPCInfoEventHandler(GameServer.NPCInfo Packet, ref PacketFlag flag);
        public event OnNPCMoveEventHandler OnNPCMove = delegate { };
        public delegate void OnNPCMoveEventHandler(GameServer.NPCMove Packet, ref PacketFlag flag);
        public event OnNPCMoveToTargetEventHandler OnNPCMoveToTarget = delegate { };
        public delegate void OnNPCMoveToTargetEventHandler(GameServer.NPCMoveToTarget Packet, ref PacketFlag flag);
        public event OnNPCStopEventHandler OnNPCStop = delegate { };
        public delegate void OnNPCStopEventHandler(GameServer.NPCStop Packet, ref PacketFlag flag);
        public event OnNPCWantsInteractEventHandler OnNPCWantsInteract = delegate { };
        public delegate void OnNPCWantsInteractEventHandler(GameServer.NPCWantsInteract Packet, ref PacketFlag flag);
        public event OnOpenWaypointEventHandler OnOpenWaypoint = delegate { };
        public delegate void OnOpenWaypointEventHandler(GameServer.OpenWaypoint Packet, ref PacketFlag flag);
        public event OnOwnedItemActionEventHandler OnOwnedItemAction = delegate { };
        public delegate void OnOwnedItemActionEventHandler(GameServer.OwnedItemAction Packet, ref PacketFlag flag);
        public event OnPartyMemberPulseEventHandler OnPartyMemberPulse = delegate { };
        public delegate void OnPartyMemberPulseEventHandler(GameServer.PartyMemberPulse Packet, ref PacketFlag flag);
        public event OnPartyMemberUpdateEventHandler OnPartyMemberUpdate = delegate { };
        public delegate void OnPartyMemberUpdateEventHandler(GameServer.PartyMemberUpdate Packet, ref PacketFlag flag);
        public event OnPartyRefreshEventHandler OnPartyRefresh = delegate { };
        public delegate void OnPartyRefreshEventHandler(GameServer.PartyRefresh Packet, ref PacketFlag flag);
        public event OnPlayerAttributeNotificationEventHandler OnPlayerAttributeNotification = delegate { };
        public delegate void OnPlayerAttributeNotificationEventHandler(GameServer.PlayerAttributeNotification Packet, ref PacketFlag flag);
        public event OnPlayerClearCursorEventHandler OnPlayerClearCursor = delegate { };
        public delegate void OnPlayerClearCursorEventHandler(GameServer.PlayerClearCursor Packet, ref PacketFlag flag);
        public event OnPlayerCorpseVisibleEventHandler OnPlayerCorpseVisible = delegate { };
        public delegate void OnPlayerCorpseVisibleEventHandler(GameServer.PlayerCorpseVisible Packet, ref PacketFlag flag);
        public event OnPlayerInGameEventHandler OnPlayerInGame = delegate { };
        public delegate void OnPlayerInGameEventHandler(GameServer.PlayerInGame Packet, ref PacketFlag flag);
        public event OnPlayerInSightEventHandler OnPlayerInSight = delegate { };
        public delegate void OnPlayerInSightEventHandler(GameServer.PlayerInSight Packet, ref PacketFlag flag);
        public event OnPlayerKillCountEventHandler OnPlayerKillCount = delegate { };
        public delegate void OnPlayerKillCountEventHandler(GameServer.PlayerKillCount Packet, ref PacketFlag flag);
        public event OnPlayerLeaveGameEventHandler OnPlayerLeaveGame = delegate { };
        public delegate void OnPlayerLeaveGameEventHandler(GameServer.PlayerLeaveGame Packet, ref PacketFlag flag);
        public event OnPlayerLifeManaChangeEventHandler OnPlayerLifeManaChange = delegate { };
        public delegate void OnPlayerLifeManaChangeEventHandler(GameServer.PlayerLifeManaChange Packet, ref PacketFlag flag);
        public event OnPlayerMoveEventHandler OnPlayerMove = delegate { };
        public delegate void OnPlayerMoveEventHandler(GameServer.PlayerMove Packet, ref PacketFlag flag);
        public event OnPlayerMoveToTargetEventHandler OnPlayerMoveToTarget = delegate { };
        public delegate void OnPlayerMoveToTargetEventHandler(GameServer.PlayerMoveToTarget Packet, ref PacketFlag flag);
        public event OnPlayerPartyRelationshipEventHandler OnPlayerPartyRelationship = delegate { };
        public delegate void OnPlayerPartyRelationshipEventHandler(GameServer.PlayerPartyRelationship Packet, ref PacketFlag flag);
        public event OnPlayerReassignEventHandler OnPlayerReassign = delegate { };
        public delegate void OnPlayerReassignEventHandler(GameServer.PlayerReassign Packet, ref PacketFlag flag);
        public event OnPlayerRelationshipEventHandler OnPlayerRelationship = delegate { };
        public delegate void OnPlayerRelationshipEventHandler(GameServer.PlayerRelationship Packet, ref PacketFlag flag);
        public event OnPlayerStopEventHandler OnPlayerStop = delegate { };
        public delegate void OnPlayerStopEventHandler(GameServer.PlayerStop Packet, ref PacketFlag flag);
        public event OnPlaySoundEventHandler OnPlaySound = delegate { };
        public delegate void OnPlaySoundEventHandler(GameServer.PlaySound Packet, ref PacketFlag flag);
        public event OnPongEventHandler OnPong = delegate { };
        public delegate void OnPongEventHandler(GameServer.Pong Packet, ref PacketFlag flag);
        public event OnPortalInfoEventHandler OnPortalInfo = delegate { };
        public delegate void OnPortalInfoEventHandler(GameServer.PortalInfo Packet, ref PacketFlag flag);
        public event OnPortalOwnershipEventHandler OnPortalOwnership = delegate { };
        public delegate void OnPortalOwnershipEventHandler(GameServer.PortalOwnership Packet, ref PacketFlag flag);
        public event OnQuestItemStateEventHandler OnQuestItemState = delegate { };
        public delegate void OnQuestItemStateEventHandler(GameServer.QuestItemState Packet, ref PacketFlag flag);
        public event OnReceiveMessageEventHandler OnReceiveMessage = delegate { };
        public delegate void OnReceiveMessageEventHandler(GameServer.GameMessage Packet, ref PacketFlag flag);
        public event OnRelator1EventHandler OnRelator1 = delegate { };
        public delegate void OnRelator1EventHandler(GameServer.Relator1 Packet, ref PacketFlag flag);
        public event OnRelator2EventHandler OnRelator2 = delegate { };
        public delegate void OnRelator2EventHandler(GameServer.Relator2 Packet, ref PacketFlag flag);
        public event OnRemoveGroundUnitEventHandler OnRemoveGroundUnit = delegate { };
        public delegate void OnRemoveGroundUnitEventHandler(GameServer.RemoveGroundUnit Packet, ref PacketFlag flag);
        public event OnReportKillEventHandler OnReportKill = delegate { };
        public delegate void OnReportKillEventHandler(GameServer.ReportKill Packet, ref PacketFlag flag);
        public event OnRequestLogonInfoEventHandler OnRequestLogonInfo = delegate { };
        public delegate void OnRequestLogonInfoEventHandler(GameServer.RequestLogonInfo Packet, ref PacketFlag flag);
        public event OnSetGameObjectModeEventHandler OnSetGameObjectMode = delegate { };
        public delegate void OnSetGameObjectModeEventHandler(GameServer.SetGameObjectMode Packet, ref PacketFlag flag);
        public event OnSetItemStateEventHandler OnSetItemState = delegate { };
        public delegate void OnSetItemStateEventHandler(GameServer.SetItemState Packet, ref PacketFlag flag);
        public event OnSetNPCModeEventHandler OnSetNPCMode = delegate { };
        public delegate void OnSetNPCModeEventHandler(GameServer.SetNPCMode Packet, ref PacketFlag flag);
        public event OnSetStateEventHandler OnSetState = delegate { };
        public delegate void OnSetStateEventHandler(GameServer.SetState Packet, ref PacketFlag flag);
        public event OnSkillsLogEventHandler OnSkillsLog = delegate { };
        public delegate void OnSkillsLogEventHandler(GameServer.SkillsLog Packet, ref PacketFlag flag);
        public event OnSmallGoldAddEventHandler OnSmallGoldAdd = delegate { };
        public delegate void OnSmallGoldAddEventHandler(GameServer.SmallGoldAdd Packet, ref PacketFlag flag);
        public event OnSummonActionEventHandler OnSummonAction = delegate { };
        public delegate void OnSummonActionEventHandler(GameServer.SummonAction Packet, ref PacketFlag flag);
        public event OnSwitchWeaponSetEventHandler OnSwitchWeaponSet = delegate { };
        public delegate void OnSwitchWeaponSetEventHandler(GameServer.SwitchWeaponSet Packet, ref PacketFlag flag);
        public event OnTransactionCompleteEventHandler OnTransactionComplete = delegate { };
        public delegate void OnTransactionCompleteEventHandler(GameServer.TransactionComplete Packet, ref PacketFlag flag);
        public event OnUnitUseSkillEventHandler OnUnitUseSkill = delegate { };
        public delegate void OnUnitUseSkillEventHandler(GameServer.UnitUseSkill Packet, ref PacketFlag flag);
        public event OnUnitUseSkillOnTargetEventHandler OnUnitUseSkillOnTarget = delegate { };
        public delegate void OnUnitUseSkillOnTargetEventHandler(GameServer.UnitUseSkillOnTarget Packet, ref PacketFlag flag);
        public event OnUnloadDoneEventHandler OnUnloadDone = delegate { };
        public delegate void OnUnloadDoneEventHandler(GameServer.UnloadDone Packet, ref PacketFlag flag);
        public event OnUpdateGameQuestLogEventHandler OnUpdateGameQuestLog = delegate { };
        public delegate void OnUpdateGameQuestLogEventHandler(GameServer.UpdateGameQuestLog Packet, ref PacketFlag flag);
        public event OnUpdateItemStatsEventHandler OnUpdateItemStats = delegate { };
        public delegate void OnUpdateItemStatsEventHandler(GameServer.UpdateItemStats Packet, ref PacketFlag flag);
        public event OnUpdateItemUIEventHandler OnUpdateItemUI = delegate { };
        public delegate void OnUpdateItemUIEventHandler(GameServer.UpdateItemUI Packet, ref PacketFlag flag);
        public event OnUpdatePlayerItemSkillEventHandler OnUpdatePlayerItemSkill = delegate { };
        public delegate void OnUpdatePlayerItemSkillEventHandler(GameServer.UpdatePlayerItemSkill Packet, ref PacketFlag flag);
        public event OnUpdateQuestInfoEventHandler OnUpdateQuestInfo = delegate { };
        public delegate void OnUpdateQuestInfoEventHandler(GameServer.UpdateQuestInfo Packet, ref PacketFlag flag);
        public event OnUpdateQuestLogEventHandler OnUpdateQuestLog = delegate { };
        public delegate void OnUpdateQuestLogEventHandler(GameServer.UpdateQuestLog Packet, ref PacketFlag flag);
        public event OnUpdateSkillEventHandler OnUpdateSkill = delegate { };
        public delegate void OnUpdateSkillEventHandler(GameServer.UpdateSkill Packet, ref PacketFlag flag);
        public event OnUseSpecialItemEventHandler OnUseSpecialItem = delegate { };
        public delegate void OnUseSpecialItemEventHandler(GameServer.UseSpecialItem Packet, ref PacketFlag flag);
        public event OnUseStackableItemEventHandler OnUseStackableItem = delegate { };
        public delegate void OnUseStackableItemEventHandler(GameServer.UseStackableItem Packet, ref PacketFlag flag);
        public event OnWalkVerifyEventHandler OnWalkVerify = delegate { };
        public delegate void OnWalkVerifyEventHandler(GameServer.WalkVerify Packet, ref PacketFlag flag);
        public event OnWardenCheckEventHandler OnWardenCheck = delegate { };
        public delegate void OnWardenCheckEventHandler(GameServer.WardenCheck Packet, ref PacketFlag flag);
        public event OnWorldItemActionEventHandler OnWorldItemAction = delegate { };
        public delegate void OnWorldItemActionEventHandler(GameServer.WorldItemAction Packet, ref PacketFlag flag);

        #endregion

        #region Diablo to Battle Net Events

            public event OnGameLogonRequestEventHandler OnGameLogonRequest = delegate { };
            public delegate void OnGameLogonRequestEventHandler(GameClient.GameLogonRequest Packet, ref PacketFlag flag);
            public event OnWalkToLocationEventHandler OnWalkToLocation = delegate { };  
            public delegate void OnWalkToLocationEventHandler(GameClient.WalkToLocation Packet, ref PacketFlag flag);
            public event OnWalkToTargetEventHandler OnWalkToTarget = delegate { };  
            public delegate void OnWalkToTargetEventHandler(GameClient.WalkToTarget Packet, ref PacketFlag flag);
            public event OnRunToLocationEventHandler OnRunToLocation = delegate { };  
            public delegate void OnRunToLocationEventHandler(GameClient.RunToLocation Packet, ref PacketFlag flag);
            public event OnRunToTargetEventHandler OnRunToTarget = delegate { };  
            public delegate void OnRunToTargetEventHandler(GameClient.RunToTarget Packet, ref PacketFlag flag);
            public event OnCastLeftSkillEventHandler OnCastLeftSkill = delegate { };  
            public delegate void OnCastLeftSkillEventHandler(GameClient.CastLeftSkill Packet, ref PacketFlag flag);
            public event OnCastLeftSkillOnTargetEventHandler OnCastLeftSkillOnTarget = delegate { };  
            public delegate void OnCastLeftSkillOnTargetEventHandler(GameClient.CastLeftSkillOnTarget Packet, ref PacketFlag flag);
            public event OnCastLeftSkillOnTargetStoppedEventHandler OnCastLeftSkillOnTargetStopped = delegate { };  
            public delegate void OnCastLeftSkillOnTargetStoppedEventHandler(GameClient.CastLeftSkillOnTargetStopped Packet, ref PacketFlag flag);
            public event OnRecastLeftSkillEventHandler OnRecastLeftSkill = delegate { };  
            public delegate void OnRecastLeftSkillEventHandler(GameClient.RecastLeftSkill Packet, ref PacketFlag flag);
            public event OnRecastLeftSkillOnTargetEventHandler OnRecastLeftSkillOnTarget = delegate { };  
            public delegate void OnRecastLeftSkillOnTargetEventHandler(GameClient.RecastLeftSkillOnTarget Packet, ref PacketFlag flag);
            public event OnRecastLeftSkillOnTargetStoppedEventHandler OnRecastLeftSkillOnTargetStopped = delegate { };  
            public delegate void OnRecastLeftSkillOnTargetStoppedEventHandler(GameClient.RecastLeftSkillOnTargetStopped Packet, ref PacketFlag flag);
            public event OnCastRightSkillEventHandler OnCastRightSkill = delegate { };  
            public delegate void OnCastRightSkillEventHandler(GameClient.CastRightSkill Packet, ref PacketFlag flag);
            public event OnCastRightSkillOnTargetEventHandler OnCastRightSkillOnTarget = delegate { };  
            public delegate void OnCastRightSkillOnTargetEventHandler(GameClient.CastRightSkillOnTarget Packet, ref PacketFlag flag);
            public event OnCastRightSkillOnTargetStoppedEventHandler OnCastRightSkillOnTargetStopped = delegate { };  
            public delegate void OnCastRightSkillOnTargetStoppedEventHandler(GameClient.CastRightSkillOnTargetStopped Packet, ref PacketFlag flag);
            public event OnRecastRightSkillEventHandler OnRecastRightSkill = delegate { };  
            public delegate void OnRecastRightSkillEventHandler(GameClient.RecastRightSkill Packet, ref PacketFlag flag);
            public event OnRecastRightSkillOnTargetEventHandler OnRecastRightSkillOnTarget = delegate { };  
            public delegate void OnRecastRightSkillOnTargetEventHandler(GameClient.RecastRightSkillOnTarget Packet, ref PacketFlag flag);
            public event OnRecastRightSkillOnTargetStoppedEventHandler OnRecastRightSkillOnTargetStopped = delegate { };  
            public delegate void OnRecastRightSkillOnTargetStoppedEventHandler(GameClient.RecastRightSkillOnTargetStopped Packet, ref PacketFlag flag);
            public event OnUnitInteractEventHandler OnUnitInteract = delegate { };  
            public delegate void OnUnitInteractEventHandler(GameClient.UnitInteract Packet, ref PacketFlag flag);
            public event OnSendOverheadMessageEventHandler OnSendOverheadMessage = delegate { };  
            public delegate void OnSendOverheadMessageEventHandler(GameClient.SendOverheadMessage Packet, ref PacketFlag flag);
            public event OnSendMessageEventHandler OnSendMessage = delegate { };  
            public delegate void OnSendMessageEventHandler(GameClient.SendMessage Packet, ref PacketFlag flag);
            public event OnPickItemEventHandler OnPickItem = delegate { };  
            public delegate void OnPickItemEventHandler(GameClient.PickItem Packet, ref PacketFlag flag);
            public event OnDropItemEventHandler OnDropItem = delegate { };  
            public delegate void OnDropItemEventHandler(GameClient.DropItem Packet, ref PacketFlag flag);
            public event OnDropItemToContainerEventHandler OnDropItemToContainer = delegate { };  
            public delegate void OnDropItemToContainerEventHandler(GameClient.DropItemToContainer Packet, ref PacketFlag flag);
            public event OnPickItemFromContainerEventHandler OnPickItemFromContainer = delegate { };  
            public delegate void OnPickItemFromContainerEventHandler(GameClient.PickItemFromContainer Packet, ref PacketFlag flag);
            public event OnEquipItemEventHandler OnEquipItem = delegate { };  
            public delegate void OnEquipItemEventHandler(GameClient.EquipItem Packet, ref PacketFlag flag);
            public event OnUnequipItemEventHandler OnUnequipItem = delegate { };  
            public delegate void OnUnequipItemEventHandler(GameClient.UnequipItem Packet, ref PacketFlag flag);
            public event OnSwapEquippedItemEventHandler OnSwapEquippedItem = delegate { };  
            public delegate void OnSwapEquippedItemEventHandler(GameClient.SwapEquippedItem Packet, ref PacketFlag flag);
            public event OnSwapContainerItemEventHandler OnSwapContainerItem = delegate { };  
            public delegate void OnSwapContainerItemEventHandler(GameClient.SwapContainerItem Packet, ref PacketFlag flag);
            public event OnUseInventoryItemEventHandler OnUseInventoryItem = delegate { };  
            public delegate void OnUseInventoryItemEventHandler(GameClient.UseInventoryItem Packet, ref PacketFlag flag);
            public event OnStackItemsEventHandler OnStackItems = delegate { };  
            public delegate void OnStackItemsEventHandler(GameClient.StackItems Packet, ref PacketFlag flag);
            public event OnAddBeltItemEventHandler OnAddBeltItem = delegate { };  
            public delegate void OnAddBeltItemEventHandler(GameClient.AddBeltItem Packet, ref PacketFlag flag);
            public event OnRemoveBeltItemEventHandler OnRemoveBeltItem = delegate { };  
            public delegate void OnRemoveBeltItemEventHandler(GameClient.RemoveBeltItem Packet, ref PacketFlag flag);
            public event OnSwapBeltItemEventHandler OnSwapBeltItem = delegate { };  
            public delegate void OnSwapBeltItemEventHandler(GameClient.SwapBeltItem Packet, ref PacketFlag flag);
            public event OnUseBeltItemEventHandler OnUseBeltItem = delegate { };  
            public delegate void OnUseBeltItemEventHandler(GameClient.UseBeltItem Packet, ref PacketFlag flag);
            public event OnIdentifyItemEventHandler OnIdentifyItem = delegate { };  
            public delegate void OnIdentifyItemEventHandler(GameClient.IdentifyItem Packet, ref PacketFlag flag);
            public event OnEmbedItemEventHandler OnEmbedItem = delegate { };  
            public delegate void OnEmbedItemEventHandler(GameClient.EmbedItem Packet, ref PacketFlag flag);
            public event OnItemToCubeEventHandler OnItemToCube = delegate { };  
            public delegate void OnItemToCubeEventHandler(GameClient.ItemToCube Packet, ref PacketFlag flag);
            public event OnTownFolkInteractEventHandler OnTownFolkInteract = delegate { };  
            public delegate void OnTownFolkInteractEventHandler(GameClient.TownFolkInteract Packet, ref PacketFlag flag);
            public event OnTownFolkCancelInteractionEventHandler OnTownFolkCancelInteraction = delegate { };  
            public delegate void OnTownFolkCancelInteractionEventHandler(GameClient.TownFolkCancelInteraction Packet, ref PacketFlag flag);
            public event OnDisplayQuestMessageEventHandler OnDisplayQuestMessage = delegate { };  
            public delegate void OnDisplayQuestMessageEventHandler(GameClient.DisplayQuestMessage Packet, ref PacketFlag flag);
            public event OnBuyItemEventHandler OnBuyItem = delegate { };  
            public delegate void OnBuyItemEventHandler(GameClient.BuyItem Packet, ref PacketFlag flag);
            public event OnSellItemEventHandler OnSellItem = delegate { };  
            public delegate void OnSellItemEventHandler(GameClient.SellItem Packet, ref PacketFlag flag);
            public event OnCainIdentifyItemsEventHandler OnCainIdentifyItems = delegate { };  
            public delegate void OnCainIdentifyItemsEventHandler(GameClient.CainIdentifyItems Packet, ref PacketFlag flag);
            public event OnTownFolkRepairEventHandler OnTownFolkRepair = delegate { };  
            public delegate void OnTownFolkRepairEventHandler(GameClient.TownFolkRepair Packet, ref PacketFlag flag);
            public event OnHireMercenaryEventHandler OnHireMercenary = delegate { };  
            public delegate void OnHireMercenaryEventHandler(GameClient.HireMercenary Packet, ref PacketFlag flag);
            public event OnIdentifyGambleItemEventHandler OnIdentifyGambleItem = delegate { };  
            public delegate void OnIdentifyGambleItemEventHandler(GameClient.IdentifyGambleItem Packet, ref PacketFlag flag);
            public event OnTownFolkMenuSelectEventHandler OnTownFolkMenuSelect = delegate { };  
            public delegate void OnTownFolkMenuSelectEventHandler(GameClient.TownFolkMenuSelect Packet, ref PacketFlag flag);
            public event OnIncrementAttributeEventHandler OnIncrementAttribute = delegate { };  
            public delegate void OnIncrementAttributeEventHandler(GameClient.IncrementAttribute Packet, ref PacketFlag flag);
            public event OnIncrementSkillEventHandler OnIncrementSkill = delegate { };  
            public delegate void OnIncrementSkillEventHandler(GameClient.IncrementSkill Packet, ref PacketFlag flag);
            public event OnSelectSkillEventHandler OnSelectSkill = delegate { };  
            public delegate void OnSelectSkillEventHandler(GameClient.SelectSkill Packet, ref PacketFlag flag);
            public event OnHoverUnitEventHandler OnHoverUnit = delegate { };  
            public delegate void OnHoverUnitEventHandler(GameClient.HoverUnit Packet, ref PacketFlag flag);
            public event OnSendCharacterSpeechEventHandler OnSendCharacterSpeech = delegate { };  
            public delegate void OnSendCharacterSpeechEventHandler(GameClient.SendCharacterSpeech Packet, ref PacketFlag flag);
            public event OnRequestQuestLogEventHandler OnRequestQuestLog = delegate { };  
            public delegate void OnRequestQuestLogEventHandler(GameClient.RequestQuestLog Packet, ref PacketFlag flag);
            public event OnRespawnEventHandler OnRespawn = delegate { };  
            public delegate void OnRespawnEventHandler(GameClient.Respawn Packet, ref PacketFlag flag);
            public event OnWaypointInteractEventHandler OnWaypointInteract = delegate { };  
            public delegate void OnWaypointInteractEventHandler(GameClient.WaypointInteract Packet, ref PacketFlag flag);
            public event OnRequestReassignEventHandler OnRequestReassign = delegate { };  
            public delegate void OnRequestReassignEventHandler(GameClient.RequestReassign Packet, ref PacketFlag flag);
            public event OnClickButtonEventHandler OnClickButton = delegate { };  
            public delegate void OnClickButtonEventHandler(GameClient.ClickButton Packet, ref PacketFlag flag);
            public event OnDropGoldEventHandler OnDropGold = delegate { };  
            public delegate void OnDropGoldEventHandler(GameClient.DropGold Packet, ref PacketFlag flag);
            public event OnSetSkillHotkeyEventHandler OnSetSkillHotkey = delegate { };  
            public delegate void OnSetSkillHotkeyEventHandler(GameClient.SetSkillHotkey Packet, ref PacketFlag flag);
            public event OnCloseQuestEventHandler OnCloseQuest = delegate { };  
            public delegate void OnCloseQuestEventHandler(GameClient.CloseQuest Packet, ref PacketFlag flag);
            public event OnGoToTownFolkEventHandler OnGoToTownFolk = delegate { };  
            public delegate void OnGoToTownFolkEventHandler(GameClient.GoToTownFolk Packet, ref PacketFlag flag);
            public event OnSetPlayerRelationEventHandler OnSetPlayerRelation = delegate { };  
            public delegate void OnSetPlayerRelationEventHandler(GameClient.SetPlayerRelation Packet, ref PacketFlag flag);
            public event OnPartyRequestEventHandler OnPartyRequest = delegate { };  
            public delegate void OnPartyRequestEventHandler(GameClient.PartyRequest Packet, ref PacketFlag flag);
            public event OnUpdatePositionEventHandler OnUpdatePosition = delegate { };  
            public delegate void OnUpdatePositionEventHandler(GameClient.UpdatePosition Packet, ref PacketFlag flag);
            public event OnSwitchWeaponsEventHandler OnSwitchWeapons = delegate { };  
            public delegate void OnSwitchWeaponsEventHandler(GameClient.SwitchWeapons Packet, ref PacketFlag flag);
            public event OnChangeMercEquipmentEventHandler OnChangeMercEquipment = delegate { };  
            public delegate void OnChangeMercEquipmentEventHandler(GameClient.ChangeMercEquipment Packet, ref PacketFlag flag);
            public event OnResurrectMercEventHandler OnResurrectMerc = delegate { };  
            public delegate void OnResurrectMercEventHandler(GameClient.ResurrectMerc Packet, ref PacketFlag flag);
            public event OnInventoryItemToBeltEventHandler OnInventoryItemToBelt = delegate { };  
            public delegate void OnInventoryItemToBeltEventHandler(GameClient.InventoryItemToBelt Packet, ref PacketFlag flag);
            public event OnWardenResponseEventHandler OnWardenResponse = delegate { };  
            public delegate void OnWardenResponseEventHandler(GameClient.WardenResponse Packet, ref PacketFlag flag);
            public event OnExitGameEventHandler OnExitGame = delegate { };  
            public delegate void OnExitGameEventHandler(GameClient.ExitGame Packet, ref PacketFlag flag);
            public event OnEnterGameEventHandler OnEnterGame = delegate { };  
            public delegate void OnEnterGameEventHandler(GameClient.EnterGame Packet, ref PacketFlag flag);
            public event OnPingEventHandler OnPing = delegate { };  
            public delegate void OnPingEventHandler(GameClient.Ping Packet, ref PacketFlag flag);
            public event OnGoToLocationEventHandler OnGoToLocation = delegate { };  
            public delegate void OnGoToLocationEventHandler(GameClient.GoToLocation Packet, ref PacketFlag flag);
            public event OnGoToTargetEventHandler OnGoToTarget = delegate { };  
            public delegate void OnGoToTargetEventHandler(GameClient.GoToTarget Packet, ref PacketFlag flag);

        #endregion

        public Proxy Proxy { get; set; }

        public GameProxy(TcpClient client) : base(client)
        {
            this.isCompressed = true;
            RealmProxy realmProxy = ProxyServer.FindRealmProxyForGame(string.Empty);
            base.Connect(realmProxy.GameAddress, realmProxy.GamePort);
            this.OnGameLogonRequest += new OnGameLogonRequestEventHandler(GameProxy_OnGameLogonRequest);
            this.OnSendMessage += new OnSendMessageEventHandler(GameProxy_OnSendMessage);

            base.Init();
        }

        void GameProxy_OnSendMessage(GameClient.SendMessage Packet, ref PacketFlag flag)
        {
            flag = PacketFlag.PacketFlag_Hidden;
            this.CompressSendToDiablo(GameServer.GameMessage.Build(D2Data.GameMessageType.GameMessage, "BlueVex2", "Hello World"));
        }

        void GameProxy_OnGameLogonRequest(GameClient.GameLogonRequest Packet, ref PacketFlag flag)
        {
            string playerName = Packet.Name;
            RealmProxy realmProxy = ProxyServer.FindRealmProxyForGame(playerName);
            realmProxy.Proxy.GameProxy = this;
            this.Proxy = realmProxy.Proxy;
        }

        protected override void DiabloToBattleNet(byte[] data, ref PacketFlag flag)
        {
            GameClientPacket packetId = (GameClientPacket)data[0];

            try
            {
                switch (packetId)
                {
                    case GameClientPacket.GameLogonRequest: OnGameLogonRequest(new GameClient.GameLogonRequest(data), ref flag); break;
                    case GameClientPacket.WalkToLocation: OnWalkToLocation(new GameClient.WalkToLocation(data), ref flag); break;
                    case GameClientPacket.WalkToTarget: OnWalkToTarget(new GameClient.WalkToTarget(data), ref flag); break;
                    case GameClientPacket.RunToLocation: OnRunToLocation(new GameClient.RunToLocation(data), ref flag); break;
                    case GameClientPacket.RunToTarget: OnRunToTarget(new GameClient.RunToTarget(data), ref flag); break;
                    case GameClientPacket.CastLeftSkill: OnCastLeftSkill(new GameClient.CastLeftSkill(data), ref flag); break;
                    case GameClientPacket.CastLeftSkillOnTarget: OnCastLeftSkillOnTarget(new GameClient.CastLeftSkillOnTarget(data), ref flag); break;
                    case GameClientPacket.CastLeftSkillOnTargetStopped: OnCastLeftSkillOnTargetStopped(new GameClient.CastLeftSkillOnTargetStopped(data), ref flag); break;
                    case GameClientPacket.RecastLeftSkill: OnRecastLeftSkill(new GameClient.RecastLeftSkill(data), ref flag); break;
                    case GameClientPacket.RecastLeftSkillOnTarget: OnRecastLeftSkillOnTarget(new GameClient.RecastLeftSkillOnTarget(data), ref flag); break;
                    case GameClientPacket.RecastLeftSkillOnTargetStopped: OnRecastLeftSkillOnTargetStopped(new GameClient.RecastLeftSkillOnTargetStopped(data), ref flag); break;
                    case GameClientPacket.CastRightSkill: OnCastRightSkill(new GameClient.CastRightSkill(data), ref flag); break;
                    case GameClientPacket.CastRightSkillOnTarget: OnCastRightSkillOnTarget(new GameClient.CastRightSkillOnTarget(data), ref flag); break;
                    case GameClientPacket.CastRightSkillOnTargetStopped: OnCastRightSkillOnTargetStopped(new GameClient.CastRightSkillOnTargetStopped(data), ref flag); break;
                    case GameClientPacket.RecastRightSkill: OnRecastRightSkill(new GameClient.RecastRightSkill(data), ref flag); break;
                    case GameClientPacket.RecastRightSkillOnTarget: OnRecastRightSkillOnTarget(new GameClient.RecastRightSkillOnTarget(data), ref flag); break;
                    case GameClientPacket.RecastRightSkillOnTargetStopped: OnRecastRightSkillOnTargetStopped(new GameClient.RecastRightSkillOnTargetStopped(data), ref flag); break;
                    case GameClientPacket.UnitInteract: OnUnitInteract(new GameClient.UnitInteract(data), ref flag); break;
                    case GameClientPacket.SendOverheadMessage: OnSendOverheadMessage(new GameClient.SendOverheadMessage(data), ref flag); break;
                    case GameClientPacket.SendMessage: OnSendMessage(new GameClient.SendMessage(data), ref flag); break;
                    case GameClientPacket.PickItem: OnPickItem(new GameClient.PickItem(data), ref flag); break;
                    case GameClientPacket.DropItem: OnDropItem(new GameClient.DropItem(data), ref flag); break;
                    case GameClientPacket.DropItemToContainer: OnDropItemToContainer(new GameClient.DropItemToContainer(data), ref flag); break;
                    case GameClientPacket.PickItemFromContainer: OnPickItemFromContainer(new GameClient.PickItemFromContainer(data), ref flag); break;
                    case GameClientPacket.EquipItem: OnEquipItem(new GameClient.EquipItem(data), ref flag); break;
                    case GameClientPacket.UnequipItem: OnUnequipItem(new GameClient.UnequipItem(data), ref flag); break;
                    case GameClientPacket.SwapEquippedItem: OnSwapEquippedItem(new GameClient.SwapEquippedItem(data), ref flag); break;
                    case GameClientPacket.SwapContainerItem: OnSwapContainerItem(new GameClient.SwapContainerItem(data), ref flag); break;
                    case GameClientPacket.UseInventoryItem: OnUseInventoryItem(new GameClient.UseInventoryItem(data), ref flag); break;
                    case GameClientPacket.StackItems: OnStackItems(new GameClient.StackItems(data), ref flag); break;
                    case GameClientPacket.AddBeltItem: OnAddBeltItem(new GameClient.AddBeltItem(data), ref flag); break;
                    case GameClientPacket.RemoveBeltItem: OnRemoveBeltItem(new GameClient.RemoveBeltItem(data), ref flag); break;
                    case GameClientPacket.SwapBeltItem: OnSwapBeltItem(new GameClient.SwapBeltItem(data), ref flag); break;
                    case GameClientPacket.UseBeltItem: OnUseBeltItem(new GameClient.UseBeltItem(data), ref flag); break;
                    case GameClientPacket.IdentifyItem: OnIdentifyItem(new GameClient.IdentifyItem(data), ref flag); break;
                    case GameClientPacket.EmbedItem: OnEmbedItem(new GameClient.EmbedItem(data), ref flag); break;
                    case GameClientPacket.ItemToCube: OnItemToCube(new GameClient.ItemToCube(data), ref flag); break;
                    case GameClientPacket.TownFolkInteract: OnTownFolkInteract(new GameClient.TownFolkInteract(data), ref flag); break;
                    case GameClientPacket.TownFolkCancelInteraction: OnTownFolkCancelInteraction(new GameClient.TownFolkCancelInteraction(data), ref flag); break;
                    case GameClientPacket.DisplayQuestMessage: OnDisplayQuestMessage(new GameClient.DisplayQuestMessage(data), ref flag); break;
                    case GameClientPacket.BuyItem: OnBuyItem(new GameClient.BuyItem(data), ref flag); break;
                    case GameClientPacket.SellItem: OnSellItem(new GameClient.SellItem(data), ref flag); break;
                    case GameClientPacket.CainIdentifyItems: OnCainIdentifyItems(new GameClient.CainIdentifyItems(data), ref flag); break;
                    case GameClientPacket.TownFolkRepair: OnTownFolkRepair(new GameClient.TownFolkRepair(data), ref flag); break;
                    case GameClientPacket.HireMercenary: OnHireMercenary(new GameClient.HireMercenary(data), ref flag); break;
                    case GameClientPacket.IdentifyGambleItem: OnIdentifyGambleItem(new GameClient.IdentifyGambleItem(data), ref flag); break;
                    case GameClientPacket.TownFolkMenuSelect: OnTownFolkMenuSelect(new GameClient.TownFolkMenuSelect(data), ref flag); break;
                    case GameClientPacket.IncrementAttribute: OnIncrementAttribute(new GameClient.IncrementAttribute(data), ref flag); break;
                    case GameClientPacket.IncrementSkill: OnIncrementSkill(new GameClient.IncrementSkill(data), ref flag); break;
                    case GameClientPacket.SelectSkill: OnSelectSkill(new GameClient.SelectSkill(data), ref flag); break;
                    case GameClientPacket.HoverUnit: OnHoverUnit(new GameClient.HoverUnit(data), ref flag); break;
                    case GameClientPacket.SendCharacterSpeech: OnSendCharacterSpeech(new GameClient.SendCharacterSpeech(data), ref flag); break;
                    case GameClientPacket.RequestQuestLog: OnRequestQuestLog(new GameClient.RequestQuestLog(data), ref flag); break;
                    case GameClientPacket.Respawn: OnRespawn(new GameClient.Respawn(data), ref flag); break;
                    case GameClientPacket.WaypointInteract: OnWaypointInteract(new GameClient.WaypointInteract(data), ref flag); break;
                    case GameClientPacket.RequestReassign: OnRequestReassign(new GameClient.RequestReassign(data), ref flag); break;
                    case GameClientPacket.ClickButton: OnClickButton(new GameClient.ClickButton(data), ref flag); break;
                    case GameClientPacket.DropGold: OnDropGold(new GameClient.DropGold(data), ref flag); break;
                    case GameClientPacket.SetSkillHotkey: OnSetSkillHotkey(new GameClient.SetSkillHotkey(data), ref flag); break;
                    case GameClientPacket.CloseQuest: OnCloseQuest(new GameClient.CloseQuest(data), ref flag); break;
                    case GameClientPacket.GoToTownFolk: OnGoToTownFolk(new GameClient.GoToTownFolk(data), ref flag); break;
                    case GameClientPacket.SetPlayerRelation: OnSetPlayerRelation(new GameClient.SetPlayerRelation(data), ref flag); break;
                    case GameClientPacket.PartyRequest: OnPartyRequest(new GameClient.PartyRequest(data), ref flag); break;
                    case GameClientPacket.UpdatePosition: OnUpdatePosition(new GameClient.UpdatePosition(data), ref flag); break;
                    case GameClientPacket.SwitchWeapons: OnSwitchWeapons(new GameClient.SwitchWeapons(data), ref flag); break;
                    case GameClientPacket.ChangeMercEquipment: OnChangeMercEquipment(new GameClient.ChangeMercEquipment(data), ref flag); break;
                    case GameClientPacket.ResurrectMerc: OnResurrectMerc(new GameClient.ResurrectMerc(data), ref flag); break;
                    case GameClientPacket.InventoryItemToBelt: OnInventoryItemToBelt(new GameClient.InventoryItemToBelt(data), ref flag); break;
                    case GameClientPacket.WardenResponse: OnWardenResponse(new GameClient.WardenResponse(data), ref flag); break;
                    case GameClientPacket.ExitGame: OnExitGame(new GameClient.ExitGame(data), ref flag); break;
                    case GameClientPacket.EnterGame: OnEnterGame(new GameClient.EnterGame(data), ref flag); break;
                    case GameClientPacket.Ping: OnPing(new GameClient.Ping(data), ref flag); break;
                    case GameClientPacket.GoToLocation: OnGoToLocation(new GameClient.GoToLocation(data), ref flag); break;
                    case GameClientPacket.GoToTarget: OnGoToTarget(new GameClient.GoToTarget(data), ref flag); break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(packetId.ToString() + ": " + ex.Message);
            }
        }

        bool FirstPacket = true;
        protected override void HandleServerReceive(byte[] data)
        {
            // decompress packets
            if (data.Length >= 2)
            {
                byte[] m_bHeader = new byte[1];
                int m_CompressedLength = 0;


                if (FirstPacket)
                {
                    FirstPacket = false;
                    PacketFlag flag = PacketFlag.PacketFlag_Normal;
                    BattleNetToDiablo(data, ref flag);

                    if (flag == PacketFlag.PacketFlag_Normal)
                    {
                        SendToDiablo(data);
                    }

                    return;
                }

                m_CompressedLength = Compression.ComputeDataLength(data);

                if (data.Length >= m_CompressedLength)
                {
                    byte[] m_CompressedData = new byte[m_CompressedLength];

                    Buffer.BlockCopy(data, 0, m_CompressedData, 0, m_CompressedLength);
                    
                    //Send to decompress
                    int DecompressionBufferLength = 24567;
                    byte[] DecompressedBytes = new byte[DecompressionBufferLength + 1];
                    int DecompressedLength = Compression.DecompressPacket(m_CompressedData, ref DecompressedBytes, DecompressionBufferLength);
                    if (DecompressedLength != 0)
                    {
                        List<byte[]> Packets = Compression.SplitPackets(DecompressedBytes, DecompressedLength);
                        if (Packets != null)
                        {
                            foreach (byte[] SplitPacket in Packets)
                            {
                                PacketFlag splitFlag = PacketFlag.PacketFlag_Normal;
                                BattleNetToDiablo(SplitPacket, ref splitFlag);

                                if (splitFlag == PacketFlag.PacketFlag_Normal)
                                {
                                    // Compress the packet again and send it to diablo
                                    
                                    //CompressSendToDiablo(SplitPacket);

                                    //int compressionBufferLength = 24567;
                                    //byte[] compressedBytes = new byte[compressionBufferLength];
                                    //int compressedLength = Compression.CompressPacket(SplitPacket, SplitPacket.Length, ref compressedBytes, compressionBufferLength);

                                    
                                    //SendToDiablo(compressedBytes, compressedLength);
                                }
                            }
                            //return;
                        }
                    }
                }

                SendToDiablo(data);
            }
        }

        public void CompressSendToDiablo(byte[] data)
        {
            int compressionBufferLength = 24567;

            // the c# conversion of the compression method does not work for some reason, so for now use the vb version.

            byte[] compressedBytes = new byte[compressionBufferLength];
            int compressedLength = Compression.CompressPacket(data, data.Length, ref compressedBytes, compressionBufferLength);

            SendToDiablo(compressedBytes, compressedLength);
        }
        
        protected override void BattleNetToDiablo(byte[] data, ref PacketFlag flag)
        {
            GameServerPacket packetId = (GameServerPacket)data[0];

            try
            {
                switch (packetId)
                {
                    case GameServerPacket.AboutPlayer: OnAboutPlayer(new GameServer.AboutPlayer(data), ref flag); break;
                    case GameServerPacket.AcceptTrade: OnAcceptTrade(new GameServer.AcceptTrade(data), ref flag); break;
                    case GameServerPacket.AddUnit: OnAddUnit(new GameServer.AddUnit(data), ref flag); break;
                    case GameServerPacket.AssignGameObject: OnAssignGameObject(new GameServer.AssignGameObject(data), ref flag); break;
                    case GameServerPacket.AssignMerc: OnAssignMerc(new GameServer.AssignMerc(data), ref flag); break;
                    case GameServerPacket.AssignNPC: OnAssignNPC(new GameServer.AssignNPC(data), ref flag); break;
                    case GameServerPacket.AssignPlayer: OnAssignPlayer(new GameServer.AssignPlayer(data), ref flag); break;
                    case GameServerPacket.AssignPlayerCorpse: OnAssignPlayerCorpse(new GameServer.AssignPlayerCorpse(data), ref flag); break;
                    case GameServerPacket.AssignPlayerToParty: OnAssignPlayerToParty(new GameServer.AssignPlayerToParty(data), ref flag); break;
                    case GameServerPacket.AssignSkill: OnAssignSkill(new GameServer.AssignSkill(data), ref flag); break;
                    case GameServerPacket.AssignSkillHotkey: OnAssignSkillHotkey(new GameServer.AssignSkillHotkey(data), ref flag); break;
                    case GameServerPacket.AssignWarp: OnAssignWarp(new GameServer.AssignWarp(data), ref flag); break;
                    case GameServerPacket.AttributeByte: OnAttributeNotification(new GameServer.AttributeByte(data), ref flag); break;
                    case GameServerPacket.AttributeDWord: OnAttributeNotification(new GameServer.AttributeDWord(data), ref flag); break;
                    case GameServerPacket.AttributeWord: OnAttributeNotification(new GameServer.AttributeWord(data), ref flag); break;
                    case GameServerPacket.ByteToExperience: OnGainExperience(new GameServer.ByteToExperience(data), ref flag); break;
                    case GameServerPacket.DelayedState: OnDelayedState(new GameServer.DelayedState(data), ref flag); break;
                    case GameServerPacket.DWordToExperience: OnGainExperience(new GameServer.DWordToExperience(data), ref flag); break;
                    case GameServerPacket.EndState: OnEndState(new GameServer.EndState(data), ref flag); break;
                    case GameServerPacket.GameHandshake: OnGameHandshake(new GameServer.GameHandshake(data), ref flag); break;
                    case GameServerPacket.GameLoading: OnGameLoading(new GameServer.GameLoading(data), ref flag); break;
                    case GameServerPacket.GameLogonReceipt: OnGameLogonReceipt(new GameServer.GameLogonReceipt(data), ref flag); break;
                    case GameServerPacket.GameLogonSuccess: OnGameLogonSuccess(new GameServer.GameLogonSuccess(data), ref flag); break;
                    case GameServerPacket.GameLogoutSuccess: OnGameLogoutSuccess(new GameServer.GameLogoutSuccess(data), ref flag); break;
                    case GameServerPacket.GameMessage: OnReceiveMessage(new GameServer.GameMessage(data), ref flag); break;
                    case GameServerPacket.GameOver: OnGameOver(new GameServer.GameOver(data), ref flag); break;
                    case GameServerPacket.GoldTrade: OnGoldTrade(new GameServer.GoldTrade(data), ref flag); break;
                    case GameServerPacket.InformationMessage: OnInformationMessage(new GameServer.InformationMessage(data), ref flag); break;
                    case GameServerPacket.ItemTriggerSkill: OnItemTriggerSkill(new GameServer.ItemTriggerSkill(data), ref flag); break;
                    case GameServerPacket.LoadAct: OnLoadAct(new GameServer.LoadAct(data), ref flag); break;
                    case GameServerPacket.LoadDone: OnLoadDone(new GameServer.LoadDone(data), ref flag); break;
                    case GameServerPacket.MapAdd: OnMapAdd(new GameServer.MapAdd(data), ref flag); break;
                    case GameServerPacket.MapRemove: OnMapRemove(new GameServer.MapRemove(data), ref flag); break;
                    case GameServerPacket.MercAttributeByte: OnMercAttributeNotification(new GameServer.MercAttributeByte(data), ref flag); break;
                    case GameServerPacket.MercAttributeDWord: OnMercAttributeNotification(new GameServer.MercAttributeDWord(data), ref flag); break;
                    case GameServerPacket.MercAttributeWord: OnMercAttributeNotification(new GameServer.MercAttributeWord(data), ref flag); break;
                    case GameServerPacket.MercByteToExperience: OnMercGainExperience(new GameServer.MercByteToExperience(data), ref flag); break;
                    case GameServerPacket.MercForHire: OnMercForHire(new GameServer.MercForHire(data), ref flag); break;
                    case GameServerPacket.MercForHireListStart: OnMercForHireListStart(new GameServer.MercForHireListStart(data), ref flag); break;
                    case GameServerPacket.MercWordToExperience: OnMercGainExperience(new GameServer.MercWordToExperience(data), ref flag); break;
                    case GameServerPacket.MonsterAttack: OnMonsterAttack(new GameServer.MonsterAttack(data), ref flag); break;
                    case GameServerPacket.NPCAction: OnNPCAction(new GameServer.NPCAction(data), ref flag); break;
                    case GameServerPacket.NPCGetHit: OnNPCGetHit(new GameServer.NPCGetHit(data), ref flag); break;
                    case GameServerPacket.NPCHeal: OnNPCHeal(new GameServer.NPCHeal(data), ref flag); break;
                    case GameServerPacket.NPCInfo: OnNPCInfo(new GameServer.NPCInfo(data), ref flag); break;
                    case GameServerPacket.NPCMove: OnNPCMove(new GameServer.NPCMove(data), ref flag); break;
                    case GameServerPacket.NPCMoveToTarget: OnNPCMoveToTarget(new GameServer.NPCMoveToTarget(data), ref flag); break;
                    case GameServerPacket.NPCStop: OnNPCStop(new GameServer.NPCStop(data), ref flag); break;
                    case GameServerPacket.NPCWantsInteract: OnNPCWantsInteract(new GameServer.NPCWantsInteract(data), ref flag); break;
                    case GameServerPacket.OpenWaypoint: OnOpenWaypoint(new GameServer.OpenWaypoint(data), ref flag); break;
                    case GameServerPacket.OwnedItemAction: OnOwnedItemAction(new GameServer.OwnedItemAction(data), ref flag); break;
                    case GameServerPacket.PartyMemberPulse: OnPartyMemberPulse(new GameServer.PartyMemberPulse(data), ref flag); break;
                    case GameServerPacket.PartyMemberUpdate: OnPartyMemberUpdate(new GameServer.PartyMemberUpdate(data), ref flag); break;
                    case GameServerPacket.PartyRefresh: OnPartyRefresh(new GameServer.PartyRefresh(data), ref flag); break;
                    case GameServerPacket.PlayerAttributeNotification: OnPlayerAttributeNotification(new GameServer.PlayerAttributeNotification(data), ref flag); break;
                    case GameServerPacket.PlayerClearCursor: OnPlayerClearCursor(new GameServer.PlayerClearCursor(data), ref flag); break;
                    case GameServerPacket.PlayerCorpseVisible: OnPlayerCorpseVisible(new GameServer.PlayerCorpseVisible(data), ref flag); break;
                    case GameServerPacket.PlayerInGame: OnPlayerInGame(new GameServer.PlayerInGame(data), ref flag); break;
                    case GameServerPacket.PlayerInSight: OnPlayerInSight(new GameServer.PlayerInSight(data), ref flag); break;
                    case GameServerPacket.PlayerKillCount: OnPlayerKillCount(new GameServer.PlayerKillCount(data), ref flag); break;
                    case GameServerPacket.PlayerLeaveGame: OnPlayerLeaveGame(new GameServer.PlayerLeaveGame(data), ref flag); break;
                    case GameServerPacket.PlayerLifeManaChange: OnPlayerLifeManaChange(new GameServer.PlayerLifeManaChange(data), ref flag); break;
                    case GameServerPacket.PlayerMove: OnPlayerMove(new GameServer.PlayerMove(data), ref flag); break;
                    case GameServerPacket.PlayerMoveToTarget: OnPlayerMoveToTarget(new GameServer.PlayerMoveToTarget(data), ref flag); break;
                    case GameServerPacket.PlayerPartyRelationship: OnPlayerPartyRelationship(new GameServer.PlayerPartyRelationship(data), ref flag); break;
                    case GameServerPacket.PlayerReassign: OnPlayerReassign(new GameServer.PlayerReassign(data), ref flag); break;
                    case GameServerPacket.PlayerRelationship: OnPlayerRelationship(new GameServer.PlayerRelationship(data), ref flag); break;
                    case GameServerPacket.PlayerStop: OnPlayerStop(new GameServer.PlayerStop(data), ref flag); break;
                    case GameServerPacket.PlaySound: OnPlaySound(new GameServer.PlaySound(data), ref flag); break;
                    case GameServerPacket.Pong: OnPong(new GameServer.Pong(data), ref flag); break;
                    case GameServerPacket.PortalInfo: OnPortalInfo(new GameServer.PortalInfo(data), ref flag); break;
                    case GameServerPacket.PortalOwnership: OnPortalOwnership(new GameServer.PortalOwnership(data), ref flag); break;
                    case GameServerPacket.QuestItemState: OnQuestItemState(new GameServer.QuestItemState(data), ref flag); break;
                    case GameServerPacket.Relator1: OnRelator1(new GameServer.Relator1(data), ref flag); break;
                    case GameServerPacket.Relator2: OnRelator2(new GameServer.Relator2(data), ref flag); break;
                    case GameServerPacket.RemoveGroundUnit: OnRemoveGroundUnit(new GameServer.RemoveGroundUnit(data), ref flag); break;
                    case GameServerPacket.ReportKill: OnReportKill(new GameServer.ReportKill(data), ref flag); break;
                    case GameServerPacket.RequestLogonInfo: OnRequestLogonInfo(new GameServer.RequestLogonInfo(data), ref flag); break;
                    case GameServerPacket.SetGameObjectMode: OnSetGameObjectMode(new GameServer.SetGameObjectMode(data), ref flag); break;
                    case GameServerPacket.SetItemState: OnSetItemState(new GameServer.SetItemState(data), ref flag); break;
                    case GameServerPacket.SetNPCMode: OnSetNPCMode(new GameServer.SetNPCMode(data), ref flag); break;
                    case GameServerPacket.SetState: OnSetState(new GameServer.SetState(data), ref flag); break;
                    case GameServerPacket.SkillsLog: OnSkillsLog(new GameServer.SkillsLog(data), ref flag); break;
                    case GameServerPacket.SmallGoldAdd: OnSmallGoldAdd(new GameServer.SmallGoldAdd(data), ref flag); break;
                    case GameServerPacket.SummonAction: OnSummonAction(new GameServer.SummonAction(data), ref flag); break;
                    case GameServerPacket.SwitchWeaponSet: OnSwitchWeaponSet(new GameServer.SwitchWeaponSet(data), ref flag); break;
                    case GameServerPacket.TransactionComplete: OnTransactionComplete(new GameServer.TransactionComplete(data), ref flag); break;
                    case GameServerPacket.UnitUseSkill: OnUnitUseSkill(new GameServer.UnitUseSkill(data), ref flag); break;
                    case GameServerPacket.UnitUseSkillOnTarget: OnUnitUseSkillOnTarget(new GameServer.UnitUseSkillOnTarget(data), ref flag); break;
                    case GameServerPacket.UnloadDone: OnUnloadDone(new GameServer.UnloadDone(data), ref flag); break;
                    case GameServerPacket.UpdateGameQuestLog: OnUpdateGameQuestLog(new GameServer.UpdateGameQuestLog(data), ref flag); break;
                    case GameServerPacket.UpdateItemStats: OnUpdateItemStats(new GameServer.UpdateItemStats(data), ref flag); break;
                    case GameServerPacket.UpdateItemUI: OnUpdateItemUI(new GameServer.UpdateItemUI(data), ref flag); break;
                    case GameServerPacket.UpdatePlayerItemSkill: OnUpdatePlayerItemSkill(new GameServer.UpdatePlayerItemSkill(data), ref flag); break;
                    case GameServerPacket.UpdateQuestInfo: OnUpdateQuestInfo(new GameServer.UpdateQuestInfo(data), ref flag); break;
                    case GameServerPacket.UpdateQuestLog: OnUpdateQuestLog(new GameServer.UpdateQuestLog(data), ref flag); break;
                    case GameServerPacket.UpdateSkill: OnUpdateSkill(new GameServer.UpdateSkill(data), ref flag); break;
                    case GameServerPacket.UseSpecialItem: OnUseSpecialItem(new GameServer.UseSpecialItem(data), ref flag); break;
                    case GameServerPacket.UseStackableItem: OnUseStackableItem(new GameServer.UseStackableItem(data), ref flag); break;
                    case GameServerPacket.WalkVerify: OnWalkVerify(new GameServer.WalkVerify(data), ref flag); break;
                    case GameServerPacket.WardenCheck: OnWardenCheck(new GameServer.WardenCheck(data), ref flag); break;
                    case GameServerPacket.WordToExperience: OnGainExperience(new GameServer.WordToExperience(data), ref flag); break;
                    case GameServerPacket.WorldItemAction: OnWorldItemAction(new GameServer.WorldItemAction(data), ref flag); break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(packetId.ToString() + ": " + ex.Message);
            }

        }

    }
}
