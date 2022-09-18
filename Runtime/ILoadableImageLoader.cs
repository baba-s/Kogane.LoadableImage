using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Kogane
{
    /// <summary>
    /// スプライトの非同期読み込みに対応した Image を管理するコンポーネントで
    /// スプライトの非同期読み込みを行うためのインターフェイス
    /// </summary>
    public interface ILoadableImageLoader : IDisposable
    {
        //================================================================================
        // 関数
        //================================================================================
        /// <summary>
        /// 指定されたパスに存在するスプライトを非同期で読み込みます
        /// </summary>
        public UniTask<Sprite> LoadAsync( GameObject gameObject, string path );
    }
}