require('chromedriver');
const WebDriver = require('selenium-webdriver');
const { By } = require('selenium-webdriver')
const assert = require('assert')
const { Workflow, step, scenario } = require('leanrunner')


//create WebDriver instance based on your browser config;

async function main() {
    //add your code here, below is an example


    const driver = new WebDriver.Builder().forBrowser('chrome').build();

    await scenario('Math', async () => {

        await step(`Navigate to home page`, async (world) => {
            let url = 'https://cuketest.github.io/apps/bootstrap-calculator/';
            await driver.get(url);
        })

        await step('I click 1 + 1', async (world) => {

            driver.findElement(By.css('[data-key="49"]')).click();
            driver.findElement(By.css('[data-constant="SUM"]')).click();
            return driver.findElement(By.css('[data-key="49"]')).click();
        })

        await step('I click the =', async (world) => {
            await driver.findElement({ linkText: '=' }).click();
        })

        await step('I should  get the result "2"', async (world) => {
            const text = await driver.findElement({ id: 'calculator-result' }).getText();
            return assert.deepEqual(text, '2');
        })

        await step('History panel should has "1 + 1 = 2" text', async (world) => {
            const text = await driver.findElement({ id: 'calc-history-list' }).getText();
            world.attachImage(await driver.takeScreenshot());
            return assert.deepEqual(text, "1 + 1 = 2");

        })

        await step('Close the browser',async(world)=>{
            await driver.quit();
        })


    });
}

Workflow.run(main);