#version 400 core

const int maxLights = 8;

in vec3 position;
in vec2 textureCoords;
//in vec3 normal;

out vec2 pass_textureCoordinates;
out vec3 surfaceNormal;
out vec3 toLightVector;
out vec3 toCameraVector;

uniform mat4 modelMatrix;
uniform mat4 projectionMatrix;
uniform mat4 viewMatrix;
uniform vec3 lightPosition;
uniform float useFakeLighting;

//View Distance Fog
const float density = .0;
const float gradient = 0;


void main(void) {
    vec3 normal = vec3(1, 1, 1);
    vec4 worldPosition = modelMatrix * vec4(position, 1.0);
    mat4 mvpMatrix = modelMatrix * projectionMatrix * viewMatrix;
    vec4 positionRelativetoCamera = viewMatrix * worldPosition;
    pass_textureCoordinates = textureCoords;

    gl_Position = mvpMatrix * positionRelativetoCamera;

    vec3 actualNormal = normal;
    if (useFakeLighting > 0.5) {
        actualNormal = vec3(0.0, 1.0, 0.0);
    }

    surfaceNormal = (modelMatrix * vec4(actualNormal, 0.0)).xyz;
    toLightVector = lightPosition - worldPosition.xyz;

    toCameraVector = (inverse(viewMatrix) * (vec4(0.0, 0.0, 0.0, 1.0))).xyz - worldPosition.xyz;

    float distance = length(positionRelativetoCamera.xyz);

}