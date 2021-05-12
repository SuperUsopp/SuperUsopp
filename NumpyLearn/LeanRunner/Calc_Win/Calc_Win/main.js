const { step, scenario, Workflow } = require('leanrunner');
const { AppModel, Auto } = require('leanpro.win');
const { Util } = require('leanpro.common');
const assert = require('assert');

const model = AppModel.loadModel(__dirname + "\\model1.tmodel");

//Win10 计算器自动化

async function addSample() {
    await scenario('计算器数学计算：4 + 5', async () => {

        await step('点击数字4', async (world) => {
            await model.getButton("Four").click();
        })

        await step('点击加法按钮', async (world) => {
            await model.getButton("Plus").click();
        })

        await step('点击数字5', async (world) => {
            await model.getButton("Five").click();
        })

        await step('点击等于按钮', async (world) => {
            await model.getButton("Equals").click();
        })
        let expected = 4 + 5;
        await step(`验证结果，应为 ${expected}`, async (world) => {
            let name = await model.getText("Text").name(); //value is "Display is 9"
            let result = parseInt(name.substr(name.lastIndexOf(' ')))
            console.log(`"${result}"`);
            assert.equal(expected, result);
        })
    })
}

async function main() {
    Util.launchProcess("calc.exe");
    await Util.delay(1000);
    await addSample(4, 5);
}

Workflow.run(main);