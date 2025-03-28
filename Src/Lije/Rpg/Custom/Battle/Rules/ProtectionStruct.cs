
// Type: Geex.Play.Rpg.Custom.Battle.Rules.ProtectionStruct
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe


namespace Geex.Play.Rpg.Custom.Battle.Rules
{
    public struct ProtectionStruct
    {
        private short protectorIndex;
        private short protectedIndex;

        public ProtectionStruct(short protectorIndex, short protectedIndex)
        {
            this.protectorIndex = protectorIndex;
            this.protectedIndex = protectedIndex;
        }

        public short ProtectorIndex
        {
            get => this.protectorIndex;
            set => this.protectorIndex = value;
        }

        public short ProtectedIndex
        {
            get => this.protectedIndex;
            set => this.protectedIndex = value;
        }
    }
}
