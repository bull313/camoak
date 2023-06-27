using System.Collections;
using Camoak.Component.Poker;
using Camoak.Component.Poker.Table;
using Camoak.Component.Poker.Table.Subcomponent;
using Camoak.Tests.Dsl.Game;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;

namespace Camoak.Tests.IntegrationTests
{
    public class IT_PreflopBeginningButtonFold
    {
        private GameObject gameManager;
        private PokerGameComponent pokerComponent;
        private PokerTable pokerTable;
        private ActionPanel actionPanel;
        private Button foldButton;

        [UnityTest]
        public IEnumerator IT_SceneLoadsWithProperComponents_AndDoesntCrash()
        {
            yield return GameTestSetup.SetScene("HumanVBotGame");

            gameManager = GameObject.Find("GameManager");
            pokerComponent = gameManager.GetComponent<PokerGameComponent>();
            pokerTable = pokerComponent.Table;
            actionPanel = pokerTable.gameObject.GetComponentInChildren<ActionPanel>();
            foldButton = actionPanel.FoldButton;

            Assert.IsNotNull(gameManager);
            Assert.IsNotNull(pokerComponent);
            Assert.IsNotNull(pokerTable.gameObject);
            Assert.IsNotNull(actionPanel);
            Assert.IsNotNull(pokerTable.gameObject.GetComponentInChildren<CenterPot>());
            Assert.IsNotNull(pokerTable.gameObject.GetComponentInChildren<PlayerActions>());
            Assert.IsNotNull(pokerTable.gameObject.GetComponentInChildren<PlayerHoleCards>());
            Assert.IsNotNull(pokerTable.gameObject.GetComponentInChildren<PlayerPositions>());
            Assert.IsNotNull(pokerTable.gameObject.GetComponentInChildren<PlayerStacks>());
            Assert.IsNotNull(foldButton);

            yield return GameTestSetup.WAIT_SINGLE_FRAME;
            yield return GameTestSetup.WAIT_SINGLE_FRAME;
            yield return GameTestSetup.WAIT_SINGLE_FRAME;

            foldButton.onClick.Invoke();
            yield return new WaitForSeconds(3f);
        }
    }
}
