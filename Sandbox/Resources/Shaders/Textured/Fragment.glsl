#version 330 core

in vec2 p_TextureCoordinates;
in vec3 p_Normal;
in vec3 p_FragmentPosition;

out vec4 colour;

//Textures;
uniform sampler2D tex;
//Lighting
uniform vec3 lightPosition;
uniform vec3 lightColour;

void main()
{
    vec4 vertexColour = texture(tex, p_TextureCoordinates);


    //Ambient Lighting
    float ambientStrength = 0.3;
    vec3 ambient = ambientStrength * lightColour;

    //Diffuse Lighting
    vec3 normal = normalize(p_Normal);
    vec3 lightDirection = normalize(lightPosition - p_FragmentPosition);
    float diff = max(dot(normal, lightDirection), 0.0);
    vec3 diffuse = diff * lightColour;

    //Final Lighting Calculations
    vec3 result = (ambient + diffuse) * vertexColour.xyz;

    //colour = vec4(lightColour, 1);
    colour = vec4(result, 1.0);
}