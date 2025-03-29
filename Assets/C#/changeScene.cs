using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class changeScene : MonoBehaviour
{
    // 通过场景编号加载场景
    public void LoadMyScene(int sceneNumber)
    {

        StartCoroutine(PlayMySound(sceneNumber)); // 使用方法引用
        //SceneManager.LoadScene(sceneNumber);
    }

    // 通过场景名称加载场景
    public void LoadMyScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        StartCoroutine(PlayMySound(sceneName)); // 重载协程以处理字符串参数
    }

    // 协程：处理 int 类型参数
    private IEnumerator PlayMySound(int sceneNumber)
    {
        yield return new WaitForSeconds(3); // 等待3秒
        SceneManager.LoadScene(sceneNumber); // 正确拼写 SceneManager
    }

    // 协程：处理 string 类型参数
    private IEnumerator PlayMySound(string sceneName)
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(sceneName);
    }
}