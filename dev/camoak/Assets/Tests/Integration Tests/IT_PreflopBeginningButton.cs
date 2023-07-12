using System.Collections;
using System.Collections.Generic;
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
    public class IT_PreflopBeginningButton
    {
        private GameObject gameManager;
        private PokerGameComponent pokerComponent;
        private PokerTable pokerTable;
        private ActionPanel actionPanel;
        private List<Button> buttons;

        [UnityTest]
        public IEnumerator IT_SceneLoadsWithProperComponents_AndDoesntCrash()
        {
            yield return GameTestSetup.SetScene("HumanVBotGame");

            gameManager = GameObject.Find("GameManager");
            pokerComponent = gameManager.GetComponent<PokerGameComponent>();
            pokerTable = pokerComponent.Table;
            actionPanel = pokerTable.gameObject.GetComponentInChildren<ActionPanel>();
            buttons = actionPanel.Buttons;

            Assert.IsNotNull(gameManager);
            Assert.IsNotNull(pokerComponent);
            Assert.IsNotNull(pokerTable.gameObject);
            Assert.IsNotNull(actionPanel);
            Assert.IsNotNull(pokerTable.gameObject.GetComponentInChildren<CenterPot>());
            Assert.IsNotNull(pokerTable.gameObject.GetComponentInChildren<PlayerActions>());
            Assert.IsNotNull(pokerTable.gameObject.GetComponentInChildren<PlayerHoleCards>());
            Assert.IsNotNull(pokerTable.gameObject.GetComponentInChildren<PlayerPositions>());
            Assert.IsNotNull(pokerTable.gameObject.GetComponentInChildren<PlayerStacks>());
            Assert.AreEqual(2, buttons.Count);

            yield return GameTestSetup.WAIT_SINGLE_FRAME;
            yield return GameTestSetup.WAIT_SINGLE_FRAME;
            yield return GameTestSetup.WAIT_SINGLE_FRAME;

            for (int i = 0; i < buttons.Count; i++)
            {
                buttons[i].onClick.Invoke();
                yield return new WaitForSeconds(3f);
            }
        }
    }
}
