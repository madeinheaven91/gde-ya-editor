{
  # no wayland :(
  inputs.nixpkgs.url = "github:NixOS/nixpkgs/nixos-unstable";

  outputs =
    { self, nixpkgs }:
    let
      system = "x86_64-linux";
      pkgs = nixpkgs.legacyPackages.${system};
      libs = with pkgs; [
        fontconfig
        freetype
        libGL
        icu
        zlib
        libX11
        libICE
        libSM
        libXext
        libXrender
      ];
    in
    {
      devShells.${system}.default = pkgs.mkShell {
        buildInputs = libs ++ [ pkgs.dotnet-sdk_10 ];
        shellHook = ''
          export LD_LIBRARY_PATH="${pkgs.lib.makeLibraryPath libs}:$LD_LIBRARY_PATH"
		  zsh
        '';
      };
    };
}
