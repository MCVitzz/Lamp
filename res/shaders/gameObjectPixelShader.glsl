#version 400 core

in vec2 pass_textureCoordinates;
in vec3 surfaceNormal;
in vec3 toLightVector;
in vec3 toCameraVector;

out vec4 out_Color;

uniform sampler2D modelTexture;
uniform vec3 lightColour;
uniform vec3 attenuation;
uniform float shineDamper;
uniform float reflectivity;

void main(void){

    vec3 unitNormal = normalize(surfaceNormal);
    vec3 unitVectorToCamera = normalize(toCameraVector);

    vec3 totalDiffuse = vec3(0.0);
    vec3 totalSpecular = vec3(0.0);

    float distance = length(toLightVector);
    float attFactor = attenuation.x + (attenuation.y * distance) + (attenuation.z * distance * distance);
    vec3 unitLightVector = normalize(toLightVector);
    float nDotl = dot(unitNormal, unitLightVector);
    float brightness = max(nDotl, 0.0);
    vec3 lightDirection = -unitLightVector;
    vec3 reflectedLightDirection = reflect(lightDirection, unitNormal);
    float specularFactor = dot(reflectedLightDirection, unitVectorToCamera);
    specularFactor = max(specularFactor, 0.0);
    float dampedFactor = pow(specularFactor, shineDamper);
    totalDiffuse = totalDiffuse + (brightness * lightColour)/attFactor;
    totalSpecular = totalSpecular + (dampedFactor * reflectivity * lightColour)/attFactor;
    totalDiffuse = max(totalDiffuse, 0.2);

    vec4 textureColour = texture(modelTexture, pass_textureCoordinates);
    if (textureColour.a<0.5){
        discard;
    }

    out_Color =  vec4(totalDiffuse, 1.0) * textureColour + vec4(totalSpecular, 1.0);
}